namespace backend.Models.DTO.Content.Schedule
{
    public class AddExamSchedule
    {
        //public int Id { get; set; }
        public string Semester { get; set; }
        //Hard constraints
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DateTime> UnavailableDates { get; set; }
        //Soft constratints
        public List<int> GapBetweenExams { get; set; }

    }
}
