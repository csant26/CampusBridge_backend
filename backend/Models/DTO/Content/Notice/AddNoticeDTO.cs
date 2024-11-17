using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Notice
{
    public class AddNoticeDTO
    {
        [Required]
        [MaxLength(20)]
        public string NoticeId { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
    }
}
