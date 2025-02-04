using backend.Models.DTO.College;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Syllabus;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace backend.Models.DTO.Teacher
{
    public class AddTeacherDTO
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<string> CourseIds { get; set; }
        public string CollegeId {  get; set; } //Supplied for validation
    }
    [Keyless]
    public class CourseTeacherResult
    {
        public string CourseTitle { get; set; }
        public string TeacherId { get; set; }
        public string Name { get; set; }
    }
}
