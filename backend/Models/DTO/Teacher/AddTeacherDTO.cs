using backend.Models.DTO.College;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Syllabus;

namespace backend.Models.DTO.Teacher
{
    public class AddTeacherDTO
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> CourseIds { get; set; }
        public string CollegeId {  get; set; }
    }
}
