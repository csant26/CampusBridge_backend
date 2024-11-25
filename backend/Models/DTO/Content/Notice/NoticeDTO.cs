using System;
using System.Collections.Generic;

namespace backend.Models.DTO.Content.Notice
{
    public class NoticeDTO
    {
        public string NoticeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> DirectedTo { get; set; }
        public string Creator { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
