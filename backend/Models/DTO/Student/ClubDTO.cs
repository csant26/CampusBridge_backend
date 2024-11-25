using System.Collections.Generic;

namespace backend.Models.DTO.Student
{
    public class ClubDTO
    {
        public string ClubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClubHeadId { get; set; }
        public List<StudentDTO> StudentDTO {  get; set; }
    }
}
