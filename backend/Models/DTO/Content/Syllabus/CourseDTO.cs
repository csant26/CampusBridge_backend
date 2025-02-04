using backend.Models.Domain.Content.Syllabi;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Student;
using backend.Models.DTO.Teacher;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Syllabus
{
    public class CourseDTO
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string CourseObjective { get; set; }
        public bool isElective { get; set; } = false;
        public string FullMarks { get; set; }
        public string PassMarks { get; set; }
        public int CreditHour { get; set; }
        public string LabDescription { get; set; }
        public List<string> Books { get; set; }
        public List<UnitDTO> UnitsDTO { get; set; }
        public List<TeacherDTO> TeacherDTO { get; set; }
        [JsonIgnore]
        public List<StudentDTO>? StudentDTO { get; set; } = null;
        public List<AssignmentDTO>? AssignmentDTO { get; set; }
    }
}
