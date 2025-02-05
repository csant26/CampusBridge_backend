namespace backend.Models.DTO.Content.Schedule
    {
    public class ClassSession
        {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string TeacherId { get; set; }
        public int AssignedTimeSlot { get; set; }
        }

    public class TeacherScheduleResponse
        {
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public string Slot { get; set; }
        }
    public class AddTeacherScheduleRequest
        {
        public string TeacherId { get; set; }
        public List<string> ConflictWith { get; set; }
        public List<int> UnavailableSlots { get; set; }
        }
    public class TeacherSchedule
        {
        public int TeacherScheduleId { get; set; }
        public string TeacherId { get; set; }
        public string Title { get; set; }
        }

    }
