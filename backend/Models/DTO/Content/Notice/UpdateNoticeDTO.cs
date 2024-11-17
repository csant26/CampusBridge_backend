using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Notice
{
    public class UpdateNoticeDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]  
        public DateTime DateUpdated { get; set; }
    }
}
