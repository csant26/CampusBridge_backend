using backend.Models.DTO.Student;
using backend.Models.DTO.Teacher;
using backend.Models.DTO.University;

namespace backend.Models.DTO.College
{
    public class CollegeDTO
    {
        public int CollegeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public List<StudentDTO> StudentDTO { get; set; }
        public List<TeacherDTO> TeacherDTO { get; set; }
        public UniversityDTO UniversityDTO { get; set; }
    }
}
