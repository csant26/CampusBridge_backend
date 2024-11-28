namespace backend.Models.DTO.Content.Schedule
{
    public class UpdateScheduleDTO
    {
        public string Title { get; set; }
        public List<string> DirectedTo { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
    }
}
