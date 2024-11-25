using backend.Models.Domain.Colleges;
using System.Collections.Generic;

namespace backend.Models.Domain.Universities
{
    public class University
    {
        public string UniversityId { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatorId { get; set; }
        public List<College> Colleges { get; set; } //one-to-many
    }
}
