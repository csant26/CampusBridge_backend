namespace backend.Models.Domain.Content.Schedules
{
    public class ExamSchedule
    {
        public int Id { get; set; }
        public string Semester {  get; set; }
        public List<string>? GapBetweenExams { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DateTime> UnavailableDates { get; set; }
    }
}
