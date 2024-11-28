using backend.Data;
using backend.Models.Domain.Content.Schedules;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Students;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace backend.Repository.Content
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public ScheduleRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }

        public async Task<Schedule> CreateSchedule(Schedule schedule)
        {
            await campusBridgeDbContext.Schedules.AddAsync(schedule);
            await campusBridgeDbContext.SaveChangesAsync();
            return schedule;
        }
        public async Task<Schedule> CreateExamSchedule(ExamSchedule examSchedule)
        {
            var syllabus = await campusBridgeDbContext.Syllabus
                    .Where(x => x.Semester == examSchedule.Semester)
                    .Include(x => x.Courses)
                    .FirstOrDefaultAsync();
            var nonElectiveCourses = new List<Course>();
            var electiveCourses = new List<Course>();
            if (syllabus != null)
            {
                nonElectiveCourses = syllabus.Courses.Where(x => x.isElective == false)
                    .OrderBy(c=>c.CourseId)
                    .ToList();
                electiveCourses = syllabus.Courses.Where(x => x.isElective == true).ToList();
            }

            //Valid Dates
            var availableDates = new List<DateTime>();
            var totalCourses = nonElectiveCourses.Count + syllabus.AllowedElectiveNo;
            for (var date = examSchedule.StartDate; date <= examSchedule.EndDate;
                date = date.AddDays(1))
            {
                if (!examSchedule.UnavailableDates.Contains(date))
                {
                    availableDates.Add(date);
                }
            }
            if(availableDates.Count < totalCourses)
            {
                return null;
            }

            var allSchedules = new List<List<DateTime>>();
            GenerateExamScheduleRecursive(
            new List<DateTime>(), availableDates,
            totalCourses,
            allSchedules
            );

            int[] heuristic = new int[allSchedules.Count];
            var index = 0;
            //Applying heuristics to each
            foreach(var sc in allSchedules)
            {   
                var score = 0;
                for (int i = 0; i < sc.Count - 1; i++)
                {
                    if (((sc[i + 1] - sc[i]).Days) - 1 == examSchedule.GapBetweenExams[i])
                    {
                        score += 100;
                    }
                    else if(((sc[i + 1] - sc[i]).Days)-1 > examSchedule.GapBetweenExams[i])
                    {
                        score += 80;
                    }
                    if (((sc[i + 1] - sc[i]).Days) - 1 < 2)
                    {
                        score -= 100;
                    }
                }

                foreach (var date in sc)
                {
                    if (date.DayOfWeek != DayOfWeek.Saturday)
                    {
                        score += 30;
                    }
                }


                if (sc[0] == examSchedule.StartDate && sc[sc.Count-1]==examSchedule.EndDate)
                {
                    score += 60;
                }
                else if(sc[0] == examSchedule.StartDate || sc[sc.Count - 1] == examSchedule.EndDate)
                {
                    score += 35;
                }
                heuristic[index] = score;
                index++;
            }

            //Ordering the schedule
            var tempSchedule = new List<DateTime>();
            int tempHeuristic;
            for(int i = 0; i < allSchedules.Count; i++)
            {
                for(int j = i + 1; j < allSchedules.Count; j++)
                {
                    if (heuristic[i] < heuristic[j])
                    {
                        tempSchedule = allSchedules[i];
                        allSchedules[i] = allSchedules[j];
                        allSchedules[j] = tempSchedule;

                        tempHeuristic = heuristic[i];
                        heuristic[i] = heuristic[j];
                        heuristic[j] = tempHeuristic;
                    }
                }
            }

            var count = 0;
            var optimalSchedule = allSchedules[0];
            var schedule = new Schedule();

            //Handling nonelective courses.
            foreach(var course in nonElectiveCourses)
            {
                schedule = new Schedule
                {
                    Id = count+1,
                    Title = $"{course.CourseTitle} Examination {optimalSchedule[0].Year}",
                    DirectedTo = new List<string> { "Student", "Teacher", "College" },
                    Date = optimalSchedule[count],
                    Category="Examination"
                };
                await CreateSchedule(schedule);
                count++;
            }
            var finalCount = count;
            //Handling elective courses
            foreach(var course in electiveCourses)
            {
                schedule = new Schedule
                {
                    Id = count + 1,
                    Title = $"{course.CourseTitle} Examination {optimalSchedule[0].Year}",
                    DirectedTo = new List<string> { "Student", "Teacher", "College" },
                    Date = optimalSchedule[finalCount],
                    Category = "Examination"
                };
                await CreateSchedule(schedule);
                count++;
            }

            return schedule;
        }

        private void GenerateExamScheduleRecursive(
            List<DateTime> currentSchedule,
            List<DateTime> availableDates,
            int numExams,
            List<List<DateTime>> allSchedules
            )
        {
            //Base case
            if (currentSchedule.Count == numExams)
            {
                allSchedules.Add(new List<DateTime>(currentSchedule));
                return;
            }

            //Recursive case
            foreach(var date in availableDates)
            {
                if (currentSchedule.Count == 0 || date > currentSchedule.Last())
                {
                    currentSchedule.Add(date);
                    GenerateExamScheduleRecursive(currentSchedule, availableDates, 
                        numExams, allSchedules);
                    currentSchedule.RemoveAt(currentSchedule.Count-1); //Backtracking
                }
            }
        }



        public Task<Schedule> CreateStudentSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public Task<Schedule> CreateTeacherSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Schedule>> GetScheduleByRole(string Role)
        {
            var schedules = await campusBridgeDbContext
                .Schedules
                .Where(x => x.DirectedTo.Contains(Role))
                .ToListAsync();
            return schedules;
        }
        public async Task<List<Schedule>> GetScheduleByCategory(string Category)
        {
            var schedules = await campusBridgeDbContext
                .Schedules
                .Where(x =>x.Category==Category)
                .ToListAsync();
            return schedules;
        }
    }
}
