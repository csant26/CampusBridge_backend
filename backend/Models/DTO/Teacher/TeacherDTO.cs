using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Syllabi;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Teacher
{
    public class TeacherDTO
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assignment>? Assignments { get; set; } = null;
    }
}
