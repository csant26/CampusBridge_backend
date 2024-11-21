using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.DTO.College;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Syllabus;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Teacher
{
    public class TeacherDTO
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<CourseDTO> CourseDTO { get; set; }
        public List<AssignmentDTO>? AssignmentDTO { get; set; } = null;
        public List<CollegeDTO> CollegeDTO { get; set; }
    }
}
