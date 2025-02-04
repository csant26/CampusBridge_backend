using backend.Data;
using backend.Models.Domain.Content.Schedules;
using backend.Models.DTO.Content.Schedule;
using Google.OrTools.Sat;
using Microsoft.AspNetCore.Http;
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
        //public async Task<Schedule> CreateTeacherSchedule(TeacherSchedule teacherScheduleData)
        //{
        //    // User-defined inputs
        //    DateTime startDate = teacherScheduleData.StartDate;
        //    DateTime endDate = teacherScheduleData.EndDate;
        //    int hoursPerDay = Convert.ToInt32(teacherScheduleData.SlotsPerDay);
        //    int breakTimeMinutes = Convert.ToInt32(teacherScheduleData.BreakMinutes);
        //    int numTeachers = 2;
        //    int numSubjects = 3;
        //    var syllabus = await campusBridgeDbContext.Syllabus.Where(x => x.Semester == teacherScheduleData.Semester)
        //        .Include(x => x.Courses)
        //        .FirstOrDefaultAsync();
        //    string[] subjects = new string[syllabus.Courses.Count];
        //    var courseIndex = 0;
        //    foreach (var course in syllabus.Courses)
        //    {
        //        subjects[courseIndex] = course.CourseTitle;
        //        courseIndex++;
        //    } 
        //    string[] teachers = teacherScheduleData.Teachers;
        //    // Define subject maximum slots per week (soft constraint)
        //    int[] maxSlotsPerSubject = { 3, 3, 2 };
        //    bool[,] teacherAvailability = teacherScheduleData.TeacherAvailability;
        //    List<DateTime> holidays = teacherScheduleData.Holidays;

        //    // Create the solver
        //    CpModel model = new CpModel();

        //    // Compute number of days
        //    int numDays = (int)(endDate - startDate).TotalDays + 1;
        //    int slotsPerDay = hoursPerDay; // One slot per hour

        //    // Decision variable: schedule[teacher, day, slot] (1 if a teacher teaches in a slot)
        //    IntVar[,,] schedule = new IntVar[numTeachers, numDays, slotsPerDay];
        //    for (int t = 0; t < numTeachers; t++)
        //    {
        //        for (int d = 0; d < numDays; d++)
        //        {
        //            for (int s = 0; s < slotsPerDay; s++)
        //            {
        //                schedule[t, d, s] = model.NewBoolVar($"schedule_t{t}_d{d}_s{s}");
        //            }
        //        }
        //    }

        //    // Constraint: No classes on holidays
        //    for (int d = 0; d < numDays; d++)
        //    {
        //        DateTime currentDate = startDate.AddDays(d);
        //        if (holidays.Contains(currentDate))
        //        {
        //            for (int t = 0; t < numTeachers; t++)
        //            {
        //                for (int s = 0; s < slotsPerDay; s++)
        //                {
        //                    model.Add(schedule[t, d, s] == 0); // No classes on holidays
        //                }
        //            }
        //        }
        //    }

        //    // Constraint: Limit teacher availability
        //    for (int t = 0; t < numTeachers; t++)
        //    {
        //        for (int d = 0; d < numDays; d++)
        //        {
        //            bool isAvailable = teacherAvailability[t, d % 7]; // Availability pattern loops weekly
        //            if (!isAvailable)
        //            {
        //                for (int s = 0; s < slotsPerDay; s++)
        //                {
        //                    model.Add(schedule[t, d, s] == 0); // No classes if teacher is unavailable
        //                }
        //            }
        //        }
        //    }

        //    // Map subjects to available slots for teachers
        //    Dictionary<int, List<int>> teacherSubjects = new Dictionary<int, List<int>>
        //    {
        //        { 0, new List<int> { 0, 1 } }, // Teacher 1 can teach Math and Science
        //        { 1, new List<int> { 2 } }    // Teacher 2 can teach Physics
        //    };

        //    // Decision variable: which subject is taught in each slot
        //    IntVar[,,] subjectAssignment = new IntVar[numDays, slotsPerDay, numSubjects];
        //    for (int d = 0; d < numDays; d++)
        //    {
        //        for (int s = 0; s < slotsPerDay; s++)
        //        {
        //            for (int c = 0; c < numSubjects; c++)
        //            {
        //                subjectAssignment[d, s, c] = model.NewBoolVar($"subjectAssignment_d{d}_s{s}_c{c}");
        //            }
        //        }
        //    }

        //    // Ensure at most one subject is assigned per slot
        //    for (int d = 0; d < numDays; d++)
        //    {
        //        for (int s = 0; s < slotsPerDay; s++)
        //        {
        //            List<IntVar> subjectsInSlot = new List<IntVar>();
        //            for (int c = 0; c < numSubjects; c++)
        //            {
        //                subjectsInSlot.Add(subjectAssignment[d, s, c]);
        //            }
        //            model.Add(LinearExpr.Sum(subjectsInSlot) <= 1); // At most one subject per slot
        //        }
        //    }

        //    // Add soft constraint: Max slots per subject
        //    List<IntVar> softViolations = new List<IntVar>();
        //    for (int c = 0; c < numSubjects; c++)
        //    {
        //        IntVar violation = model.NewBoolVar($"violation_subject_{c}");
        //        softViolations.Add(violation);

        //        List<IntVar> totalSubjectSlots = new List<IntVar>();
        //        for (int d = 0; d < numDays; d++)
        //        {
        //            for (int s = 0; s < slotsPerDay; s++)
        //            {
        //                totalSubjectSlots.Add(subjectAssignment[d, s, c]);
        //            }
        //        }

        //        model.Add(LinearExpr.Sum(totalSubjectSlots) <= maxSlotsPerSubject[c] + violation * 1000);
        //    }

        //    // Objective: Minimize soft constraint violations
        //    model.Minimize(LinearExpr.Sum(softViolations));

        //    // Solve the model
        //    CpSolver solver = new CpSolver();
        //    CpSolverStatus status = solver.Solve(model);

        //    var teacherSchedule = new Schedule();

        //    if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
        //    {
        //        //Console.WriteLine("Schedule:");
        //        for (int d = 0; d < numDays; d++)
        //        {
        //            DateTime currentDate = startDate.AddDays(d);
        //            //Console.WriteLine($"Date: {currentDate.ToShortDateString()}");
        //            for (int s = 0; s < slotsPerDay; s++)
        //            {
        //                for (int t = 0; t < numTeachers; t++)
        //                {
        //                    if (solver.Value(schedule[t, d, s]) == 1)
        //                    {
        //                        for (int c = 0; c < numSubjects; c++)
        //                        {
        //                            if (solver.Value(subjectAssignment[d, s, c]) == 1)
        //                            {
        //                                var (startTime, endTime) = GetSlotTime(s, currentDate, slotsPerDay, breakTimeMinutes);
        //                                teacherSchedule = new Schedule
        //                                {
        //                                    Title = $"{subjects[c]} class (Teacher:{teachers[t]})",
        //                                    DirectedTo= new List<string> { "Student", "Teacher", "College" },
        //                                    StartTime = startTime,
        //                                    EndTime = endTime,
        //                                    Category = "Class Schedule"
        //                                };
        //                                await campusBridgeDbContext.Schedules.AddAsync(teacherSchedule);
        //                                await campusBridgeDbContext.SaveChangesAsync();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return teacherSchedule;
        //}
        //public (DateTime, DateTime) GetSlotTime(int slotIndex, DateTime date, int slotsPerDay, int breakDuration=30, int slotDuration = 60)
        //{
        //    // Base start time at 6 AM
        //    int baseHour = 6;
        //    int startHour = baseHour + slotIndex;
        //    DateTime startTime = date.Date.AddHours(startHour);

        //    // Calculate break after middle slot
        //    int breakAfterSlot = slotsPerDay / 2;

        //    // If the current slot index is greater than or equal to the breakAfterSlot, add the break time
        //    if (slotIndex >= breakAfterSlot)
        //    {
        //        startTime = startTime.AddMinutes(breakDuration);
        //    }

        //    // Calculate the end time for the slot
        //    DateTime endTime = startTime.AddMinutes(slotDuration); // Add slot duration (default 1 hour)

        //    return (startTime, endTime);
        //}

        public async Task<List<TeacherScheduleResponse>> CreateTeacherScheduleFromGraph(List<ClassSession> sessions)
        {
            // Sample sessions to be scheduled.
            //List<ClassSession> sessions = new List<ClassSession>
            //{
            //    new ClassSession { Id = 1, TeacherId = "101", CourseName = "Math" },
            //    new ClassSession { Id = 2, TeacherId = "101", CourseName = "Algebra" },
            //    new ClassSession { Id = 3, TeacherId = "102", CourseName = "History" },
            //    new ClassSession { Id = 4, TeacherId = "103", CourseName = "Biology" },
            //    new ClassSession { Id = 5, TeacherId = "102", CourseName = "Geography" }
            //};
            Dictionary<int, string> slotToTime = new Dictionary<int, string>()
            {
                { 0, "6:30-7:30AM" },
                { 1, "7:30-8:30AM" },
                { 2, "8:30-9:30AM" },
                { 3, "10:00-10:30AM" },
                { 4, "10:30-11:30AM" }
            };
            List<TeacherScheduleResponse> responseColl = new List<TeacherScheduleResponse>();


            int totalSlots = 5; // For example, 5 available time slots in a day.

            Scheduler scheduler = new Scheduler();


            // Add unavailable slots
            scheduler.SetUnavailableSlot("DWDMTeacherId", 0);  // Teacher 101 can't be assigned to slot 0
            scheduler.SetUnavailableSlot("DWDMTeacherId", 1);  // Teacher 101 can't be assigned to slot 1
            scheduler.SetUnavailableSlot("DWDMTeacherId", 2);  // Teacher 101 can't be assigned to slot 2

            // Add teacher time conflicts
            scheduler.SetTeacherTimeConflict("DWDMTeacherId", "SPMTeacherId");  // Teachers 101 and 102 can't teach at the same time




            // 1. Build the conflict graph based on teacher overlaps.
            scheduler.BuildGraph(sessions);

            // 2. Order sessions so that those with the most conflicts are scheduled first.
            List<ClassSession> orderedSessions = scheduler.OrderByConstraints(sessions);

            // 3. Use backtracking (with the least constraining slot heuristic) to assign slots.
            bool success = scheduler.AssignSlots(orderedSessions, totalSlots);
            if (success)
            {
                Console.WriteLine("Initial Schedule:");
                foreach (var session in orderedSessions)
                {
                    Console.WriteLine($"Session {session.Id} ({session.CourseName}, Teacher {session.TeacherId}) assigned slot {session.AssignedTimeSlot}");
                }

                // 4. Optimize the schedule to minimize gaps in teacher schedules using simulated annealing.
                List<ClassSession> optimizedSchedule = scheduler.OptimizeSchedule(orderedSessions, 1000);


                foreach (var session in optimizedSchedule)
                {
                    //Console.WriteLine($"Session {session.Id} ({session.CourseName}, Teacher {session.TeacherId}) assigned slot {session.AssignedTimeSlot}");
                    var response = new TeacherScheduleResponse()
                    {
                        CourseName = session.CourseName,
                        TeacherName = (await campusBridgeDbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == session.TeacherId)).Name,
                        Slot = (slotToTime.FirstOrDefault(x=>x.Key==session.AssignedTimeSlot)).Value
                    };
                    responseColl.Add(response);
                    var schedule = new Schedule()
                    {
                        Title = $"{session.CourseName} Class {(slotToTime.FirstOrDefault(x => x.Key == session.AssignedTimeSlot)).Value}",
                        DirectedTo = new List<string> { "Teacher" },
                        Date = DateTime.Now,
                        Category = "Teacher Schedule"
                    };
                    await campusBridgeDbContext.Schedules.AddAsync(schedule);
                    await campusBridgeDbContext.SaveChangesAsync();
                }
       
            }
            else
            {
                return null;
            }
            return responseColl;
        }
    }
    public class Scheduler
    {
        // This graph holds conflicts between sessions.
        // Key: session Id, Value: list of session Ids that conflict with the key.
        private Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

        // Constraint 1: Unavailable slots for teachers
        private Dictionary<string, List<int>> unavailableTeacherSlots = new Dictionary<string, List<int>>();

        // Constraint 2: Teachers who can't teach at the same time
        private List<(string Teacher1Id, string Teacher2Id)> teacherTimeConflicts = new List<(string, string)>(); // Method to set unavailable slots for a teacher
        public void SetUnavailableSlot(string teacherId, int slot)
        {
            if (!unavailableTeacherSlots.ContainsKey(teacherId))
                unavailableTeacherSlots[teacherId] = new List<int>();

            if (!unavailableTeacherSlots[teacherId].Contains(slot))
                unavailableTeacherSlots[teacherId].Add(slot);
        }

        // Method to set conflict between two teachers' schedules
        public void SetTeacherTimeConflict(string teacher1Id, string teacher2Id)
        {
            if (!teacherTimeConflicts.Contains((teacher1Id, teacher2Id)) &&
                !teacherTimeConflicts.Contains((teacher2Id, teacher1Id)))
            {
                teacherTimeConflicts.Add((teacher1Id, teacher2Id));
            }
        }






        /// <summary>
        /// Build a conflict graph based on the sessions.
        /// Two sessions conflict if they share the same teacher.
        /// </summary>
        public void BuildGraph(List<ClassSession> sessions)
        {
            // Initialize each session as a node in the graph.
            foreach (var session in sessions)
            {
                graph[session.Id] = new List<int>();
            }

            // For each pair of sessions, add an edge if they conflict.
            foreach (var s1 in sessions)
            {
                foreach (var s2 in sessions)
                {
                    if (s1.Id != s2.Id && HaveConflict(s1, s2))
                    {
                        if (!graph[s1.Id].Contains(s2.Id))
                            graph[s1.Id].Add(s2.Id);
                        if (!graph[s2.Id].Contains(s1.Id))
                            graph[s2.Id].Add(s1.Id);
                    }
                }
            }
        }

        // Two sessions conflict if they are taught by the same teacher.
        private bool HaveConflict(ClassSession s1, ClassSession s2)
        {
            // Conflict if the same teacher is teaching both sessions
            if (s1.TeacherId == s2.TeacherId)
                return true;

            // Conflict if the user specified these teachers can't teach at the same time
            if (teacherTimeConflicts.Contains((s1.TeacherId, s2.TeacherId)) ||
                teacherTimeConflicts.Contains((s2.TeacherId, s1.TeacherId)))
                return true;

            return false;
        }

        /// <summary>
        /// Heuristic 1: Order sessions by their degree of constraint.
        /// Sessions with more conflicts (more neighbors in the graph)
        /// are scheduled first.
        /// </summary>
        public List<ClassSession> OrderByConstraints(List<ClassSession> sessions)
        {
            return sessions.OrderByDescending(s => graph[s.Id].Count).ToList();
        }

        /// <summary>
        /// Heuristic 2: For a given session, return a list of available slots
        /// ordered by the least number of conflicts they would create.
        /// </summary>
        public List<int> GetLeastConstrainingSlots(ClassSession session, int totalSlots, int[] assignedSlots, List<ClassSession> sessions)
        {
            Dictionary<int, int> slotConflicts = new Dictionary<int, int>();
            for (int slot = 0; slot < totalSlots; slot++)
            {
                // Skip if the slot is unavailable for the teacher
                if (unavailableTeacherSlots.ContainsKey(session.TeacherId) &&
                unavailableTeacherSlots[session.TeacherId].Contains(slot))
                    continue;

                int conflictCount = 0;
                // Count how many neighbors are already assigned this slot.
                foreach (var neighborId in graph[session.Id])
                {
                    int neighborIndex = sessions.FindIndex(s => s.Id == neighborId);
                    if (assignedSlots[neighborIndex] == slot)
                        conflictCount++;
                }
                slotConflicts[slot] = conflictCount;
            }
            // Order slots so that the one with the fewest conflicts comes first.
            return slotConflicts.OrderBy(kv => kv.Value).Select(kv => kv.Key).ToList();
        }

        // Checks whether assigning a given slot to the session at the given index is valid.
        public bool IsValid(int index, int slot, int[] assignedSlots, List<ClassSession> sessions)
        {

            var session = sessions[index];
            // Check if the slot is unavailable for the teacher
            if (unavailableTeacherSlots.ContainsKey(session.TeacherId) &&
                unavailableTeacherSlots[session.TeacherId].Contains(slot))
            {
                return false;
            }


            foreach (var neighborId in graph[sessions[index].Id])
            {
                int neighborIndex = sessions.FindIndex(s => s.Id == neighborId);
                if (assignedSlots[neighborIndex] == slot)
                    return false;
            }
            return true;
        }

        // Backtracking routine to assign time slots to sessions using the least
        // constraining value heuristic.
        public bool Assign(int index, List<ClassSession> sessions, int[] assignedSlots, int totalSlots)
        {
            if (index == sessions.Count)
                return true;  // All sessions have been assigned.

            // Get the best order of slots for the current session.
            var bestSlots = GetLeastConstrainingSlots(sessions[index], totalSlots, assignedSlots, sessions);
            foreach (var slot in bestSlots)
            {
                if (IsValid(index, slot, assignedSlots, sessions))
                {
                    assignedSlots[index] = slot;
                    sessions[index].AssignedTimeSlot = slot;
                    if (Assign(index + 1, sessions, assignedSlots, totalSlots))
                        return true;
                    // Backtrack if needed.
                    assignedSlots[index] = -1;
                    sessions[index].AssignedTimeSlot = -1;
                }
            }
            return false;
        }

        // Wrapper that initializes the assignment array and kicks off the backtracking.
        public bool AssignSlots(List<ClassSession> sessions, int totalSlots)
        {
            int[] assignedSlots = new int[sessions.Count];
            for (int i = 0; i < assignedSlots.Length; i++)
                assignedSlots[i] = -1;
            return Assign(0, sessions, assignedSlots, totalSlots);
        }

        /// <summary>
        /// A penalty function that evaluates the schedule based on teacher gaps.
        /// The goal is to have as few gaps between a teacher's sessions as possible.
        /// </summary>
        public int CalculateSchedulePenalty(List<ClassSession> sessions)
        {
            int penalty = 0;
            // Group sessions by teacher.
            var teacherSchedules = sessions.GroupBy(s => s.TeacherId)
                .ToDictionary(g => g.Key, g => g.Select(s => s.AssignedTimeSlot).OrderBy(x => x).ToList());
            // For each teacher, add a penalty for each gap between sessions.
            foreach (var schedule in teacherSchedules.Values)
            {
                for (int i = 1; i < schedule.Count; i++)
                {
                    int gap = schedule[i] - schedule[i - 1] - 1;
                    if (gap > 0)
                        penalty += gap;
                }
            }
            return penalty;
        }

        /// <summary>
        /// Uses a simple simulated annealing approach to optimize the schedule.
        /// This routine randomly swaps time slots between sessions and retains changes
        /// if they reduce the penalty (i.e. reduce teacher gaps) while keeping the schedule valid.
        /// </summary>
        public List<ClassSession> OptimizeSchedule(List<ClassSession> sessions, int iterations)
        {
            List<ClassSession> bestSchedule = CloneSessions(sessions);
            int bestPenalty = CalculateSchedulePenalty(bestSchedule);
            Random rand = new Random();

            for (int i = 0; i < iterations; i++)
            {
                List<ClassSession> newSchedule = CloneSessions(bestSchedule);
                // Randomly select two sessions to swap their time slots.
                int index1 = rand.Next(newSchedule.Count);
                int index2 = rand.Next(newSchedule.Count);
                if (index1 == index2)
                    continue;

                // Swap the assigned time slots.
                int temp = newSchedule[index1].AssignedTimeSlot;
                newSchedule[index1].AssignedTimeSlot = newSchedule[index2].AssignedTimeSlot;
                newSchedule[index2].AssignedTimeSlot = temp;

                // Check whether the new schedule remains conflict free.
                if (!IsScheduleValid(newSchedule))
                    continue;

                int newPenalty = CalculateSchedulePenalty(newSchedule);
                if (newPenalty < bestPenalty)
                {
                    bestSchedule = newSchedule;
                    bestPenalty = newPenalty;
                }
            }
            return bestSchedule;
        }

        // Helper method to clone the sessions list.
        private List<ClassSession> CloneSessions(List<ClassSession> sessions)
        {
            return sessions.Select(s => new ClassSession
            {
                Id = s.Id,
                TeacherId = s.TeacherId,
                CourseName = s.CourseName,
                AssignedTimeSlot = s.AssignedTimeSlot
            }).ToList();
        }

        // Checks that the schedule is still conflict free.
        private bool IsScheduleValid(List<ClassSession> sessions)
        {
            foreach (var session in sessions)
            {
                foreach (var neighborId in graph[session.Id])
                {
                    var neighbor = sessions.First(s => s.Id == neighborId);
                    if (session.AssignedTimeSlot == neighbor.AssignedTimeSlot)
                        return false;
                }
            }
            return true;
        }
    }
}
