namespace backend.Models.DTO.Content.Notice
{
    public class AddNoticeDTO
    {
        public string NoticeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
