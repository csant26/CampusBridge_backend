using backend.Models.Domain.Content.Syllabi;
using System.Collections.Generic;

namespace backend.Models.DTO.Content.Syllabus
{
    public class SyllabusDTO
    {
        public string SyllabusId { get; set; }
        public List<CourseDTO> CourseDTO { get; set; }
        public string Semester { get; set; }
        public string AllowedElectiveNo { get; set; }
    }
}
