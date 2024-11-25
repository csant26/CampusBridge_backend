using System;
using System.Collections.Generic;
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
        public List<string> DirectedTo { get; set; }
        [Required]
        public DateTime DateUpdated { get; set; }
        public string CreatorId { get; set; } //Supplied for validation only
    }
}
