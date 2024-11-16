namespace backend.Models.Domain.Content.Notices
{
    public class Notice
    {
        public string NoticeId { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Creator {  get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
