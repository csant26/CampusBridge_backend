using System.Collections.Generic;

namespace backend.Models.DTO.Student
{
    public class UpdateClubDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClubHeadId { get; set; } //Supplied for validation
        public List<string> StudentId { get; set; }
    }
}
