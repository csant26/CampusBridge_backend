using backend.Models.DTO.Student;
using backend.Models.DTO.Teacher;
using backend.Models.DTO.University;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.College
{
    public class CollegeDTO
    {
        public string CollegeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public List<StudentDTO> StudentDTO { get; set; }
        public List<TeacherDTO> TeacherDTO { get; set; }
        public UniversityDTO UniversityDTO { get; set; }
    }
}
