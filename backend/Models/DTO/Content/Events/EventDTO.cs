using System;
using System.Collections.Generic;

namespace backend.Models.DTO.Content.Events
{
    public class EventDTO
    {
        public string EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<string> DirectedTo { get; set; }
        public string Creator { get; set; }
    }
}
