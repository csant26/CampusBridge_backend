using System;
using System.Collections.Generic;
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
        public List<string> DirectedTo { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        public string CreatorId { get; set; } //Supplied for validation only

    }
}
