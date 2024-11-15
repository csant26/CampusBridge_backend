using backend.Models.Domain.Content.Syllabi;

namespace backend.Models.DTO.Content.Syllabus
{
    public class UpdateSyllabusDTO
    {
        public string SyllabusId { get; set; }
        public List<string> CourseId { get; set; }
        public string Semester { get; set; }
        public int AllowedElectiveNo { get; set; } = 1;
    }
}
