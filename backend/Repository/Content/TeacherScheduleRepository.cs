using backend.Data;
using backend.Models.Domain.Content.Schedules;
using Google.OrTools.Sat;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class TeacherScheduleRepository : ITeacherScheduleRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public TeacherScheduleRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Schedule> CreateTeacherSchedule(TeacherSchedule teacherScheduleData)
        {
            // User-defined inputs
            DateTime startDate = teacherScheduleData.StartDate;
            DateTime endDate = teacherScheduleData.EndDate;
            int hoursPerDay = Convert.ToInt32(teacherScheduleData.SlotsPerDay);
            int breakTimeMinutes = Convert.ToInt32(teacherScheduleData.BreakMinutes);
            int numTeachers = 2;
            int numSubjects = 3;
            var syllabus = await campusBridgeDbContext.Syllabus.Where(x => x.Semester == teacherScheduleData.Semester)
                .Include(x => x.Courses)
                .FirstOrDefaultAsync();
            string[] subjects = new string[syllabus.Courses.Count];
            var courseIndex = 0;
            foreach (var course in syllabus.Courses)
            {
                subjects[courseIndex] = course.CourseTitle;
                courseIndex++;
            } 
            string[] teachers = teacherScheduleData.Teachers;
            // Define subject maximum slots per week (soft constraint)
            int[] maxSlotsPerSubject = { 3, 3, 2 };
            bool[,] teacherAvailability = teacherScheduleData.TeacherAvailability;
            List<DateTime> holidays = teacherScheduleData.Holidays;

            // Create the solver
            CpModel model = new CpModel();

            // Compute number of days
            int numDays = (int)(endDate - startDate).TotalDays + 1;
            int slotsPerDay = hoursPerDay; // One slot per hour

            // Decision variable: schedule[teacher, day, slot] (1 if a teacher teaches in a slot)
            IntVar[,,] schedule = new IntVar[numTeachers, numDays, slotsPerDay];
            for (int t = 0; t < numTeachers; t++)
            {
                for (int d = 0; d < numDays; d++)
                {
                    for (int s = 0; s < slotsPerDay; s++)
                    {
                        schedule[t, d, s] = model.NewBoolVar($"schedule_t{t}_d{d}_s{s}");
                    }
                }
            }

            // Constraint: No classes on holidays
            for (int d = 0; d < numDays; d++)
            {
                DateTime currentDate = startDate.AddDays(d);
                if (holidays.Contains(currentDate))
                {
                    for (int t = 0; t < numTeachers; t++)
                    {
                        for (int s = 0; s < slotsPerDay; s++)
                        {
                            model.Add(schedule[t, d, s] == 0); // No classes on holidays
                        }
                    }
                }
            }

            // Constraint: Limit teacher availability
            for (int t = 0; t < numTeachers; t++)
            {
                for (int d = 0; d < numDays; d++)
                {
                    bool isAvailable = teacherAvailability[t, d % 7]; // Availability pattern loops weekly
                    if (!isAvailable)
                    {
                        for (int s = 0; s < slotsPerDay; s++)
                        {
                            model.Add(schedule[t, d, s] == 0); // No classes if teacher is unavailable
                        }
                    }
                }
            }

            // Map subjects to available slots for teachers
            Dictionary<int, List<int>> teacherSubjects = new Dictionary<int, List<int>>
            {
                { 0, new List<int> { 0, 1 } }, // Teacher 1 can teach Math and Science
                { 1, new List<int> { 2 } }    // Teacher 2 can teach Physics
            };

            // Decision variable: which subject is taught in each slot
            IntVar[,,] subjectAssignment = new IntVar[numDays, slotsPerDay, numSubjects];
            for (int d = 0; d < numDays; d++)
            {
                for (int s = 0; s < slotsPerDay; s++)
                {
                    for (int c = 0; c < numSubjects; c++)
                    {
                        subjectAssignment[d, s, c] = model.NewBoolVar($"subjectAssignment_d{d}_s{s}_c{c}");
                    }
                }
            }

            // Ensure at most one subject is assigned per slot
            for (int d = 0; d < numDays; d++)
            {
                for (int s = 0; s < slotsPerDay; s++)
                {
                    List<IntVar> subjectsInSlot = new List<IntVar>();
                    for (int c = 0; c < numSubjects; c++)
                    {
                        subjectsInSlot.Add(subjectAssignment[d, s, c]);
                    }
                    model.Add(LinearExpr.Sum(subjectsInSlot) <= 1); // At most one subject per slot
                }
            }

            // Add soft constraint: Max slots per subject
            List<IntVar> softViolations = new List<IntVar>();
            for (int c = 0; c < numSubjects; c++)
            {
                IntVar violation = model.NewBoolVar($"violation_subject_{c}");
                softViolations.Add(violation);

                List<IntVar> totalSubjectSlots = new List<IntVar>();
                for (int d = 0; d < numDays; d++)
                {
                    for (int s = 0; s < slotsPerDay; s++)
                    {
                        totalSubjectSlots.Add(subjectAssignment[d, s, c]);
                    }
                }

                model.Add(LinearExpr.Sum(totalSubjectSlots) <= maxSlotsPerSubject[c] + violation * 1000);
            }

            // Objective: Minimize soft constraint violations
            model.Minimize(LinearExpr.Sum(softViolations));

            // Solve the model
            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);

            var teacherSchedule = new Schedule();

            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                //Console.WriteLine("Schedule:");
                for (int d = 0; d < numDays; d++)
                {
                    DateTime currentDate = startDate.AddDays(d);
                    //Console.WriteLine($"Date: {currentDate.ToShortDateString()}");
                    for (int s = 0; s < slotsPerDay; s++)
                    {
                        for (int t = 0; t < numTeachers; t++)
                        {
                            if (solver.Value(schedule[t, d, s]) == 1)
                            {
                                for (int c = 0; c < numSubjects; c++)
                                {
                                    if (solver.Value(subjectAssignment[d, s, c]) == 1)
                                    {
                                        var (startTime, endTime) = GetSlotTime(s, currentDate, slotsPerDay, breakTimeMinutes);
                                        teacherSchedule = new Schedule
                                        {
                                            Title = $"{subjects[c]} class (Teacher:{teachers[t]})",
                                            DirectedTo= new List<string> { "Student", "Teacher", "College" },
                                            StartTime = startTime,
                                            EndTime = endTime,
                                            Category = "Class Schedule"
                                        };
                                        await campusBridgeDbContext.Schedules.AddAsync(teacherSchedule);
                                        await campusBridgeDbContext.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return teacherSchedule;
        }
        public (DateTime, DateTime) GetSlotTime(int slotIndex, DateTime date, int slotsPerDay, int breakDuration=30, int slotDuration = 60)
        {
            // Base start time at 6 AM
            int baseHour = 6;
            int startHour = baseHour + slotIndex;
            DateTime startTime = date.Date.AddHours(startHour);

            // Calculate break after middle slot
            int breakAfterSlot = slotsPerDay / 2;

            // If the current slot index is greater than or equal to the breakAfterSlot, add the break time
            if (slotIndex >= breakAfterSlot)
            {
                startTime = startTime.AddMinutes(breakDuration);
            }

            // Calculate the end time for the slot
            DateTime endTime = startTime.AddMinutes(slotDuration); // Add slot duration (default 1 hour)

            return (startTime, endTime);
        }

    }
}
