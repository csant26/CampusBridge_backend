namespace backend.Models.DTO.Content.Events
{
    public class UpdateEventDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<string> DirectedTo { get; set; }
        public string CreatorId { get; set; }
    }
}
