namespace backend.Models.Domain.Content.Notices
{
    public class Notice
    {
        public string NoticeId { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public List<string> DirectedTo { get; set; }
        public string Creator {  get; set; }
        public string CreatorId { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
