using backend.Models.Domain.Content.Syllabus;

namespace backend.Models.DTO.Content.Syllabus
{
    public class SyllabusDTO
    {
        public string SyllabusId { get; set; }
        public List<Course> Courses { get; set; }
        public string Semester { get; set; }
    }
}
