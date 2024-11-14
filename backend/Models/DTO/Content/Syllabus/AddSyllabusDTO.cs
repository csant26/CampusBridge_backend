using backend.Models.Domain.Content.Syllabus;

namespace backend.Models.DTO.Content.Syllabus
{
    public class AddSyllabusDTO
    {
        public string SyllabusId { get; set; }
        public List<string> CourseId { get; set; }
        public string Semester { get; set; }
    }
}
