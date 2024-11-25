using backend.Models.Domain.Content.Syllabi;
using System.Collections.Generic;

namespace backend.Models.DTO.Content.Syllabus
{
    public class UpdateSyllabusDTO
    {
        public List<string> CourseId { get; set; }
        public string Semester { get; set; }
        public int AllowedElectiveNo { get; set; } = 1;
    }
}
