using backend.Models.Domain.Content.Syllabi;
using System.Collections.Generic;

namespace backend.Models.DTO.Content.Syllabus
{
    public class AddSyllabusDTO
    {
        public string SyllabusId { get; set; }
        public List<string> CourseId { get; set; }
        public string Semester { get; set; }
        public int AllowedElectiveNo { get; set; } = 1;
    }
}
