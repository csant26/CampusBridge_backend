namespace backend.Models.DTO.Content.Schedule
{
    public class AddTeacherScheduleDTO
    {
        public string Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SlotsPerDay { get; set; }
        public int BreakMinutes { get; set; }
        public string[] Teachers { get; set; }
        public bool[,] TeacherAvailability { get; set; }
        public List<DateTime> Holidays { get; set; }
    }
}
