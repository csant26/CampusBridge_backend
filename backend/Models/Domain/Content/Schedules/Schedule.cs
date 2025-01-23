namespace backend.Models.Domain.Content.Schedules
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> DirectedTo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Category {  get; set; }
    }
}
