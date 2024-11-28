namespace backend.Models.Domain.Content.Schedules
{
    public class TeacherSchedule
    {
        public int Id { get; set; }
        public string Semester {  get; set; }
        public string TeacherId { get; set; }
        public List<DateTime> UnavailableDates {  get; set; }   

    }
}
