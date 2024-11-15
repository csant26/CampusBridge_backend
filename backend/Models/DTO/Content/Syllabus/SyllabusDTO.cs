using backend.Models.Domain.Content.Syllabi;

namespace backend.Models.DTO.Content.Syllabus
{
    public class SyllabusDTO
    {
        public string SyllabusId { get; set; }
        public List<CourseDTO> CourseDTO { get; set; }
        public string Semester { get; set; }
    }
}
