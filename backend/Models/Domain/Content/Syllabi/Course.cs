using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Syllabi
{
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string CourseObjective { get; set; }
        public bool isElective { get; set; } = false;
        public string FullMarks { get; set; }
        public string PassMarks { get; set; }
        public int CreditHour { get; set; }
        public string LabDescription {  get; set; }
        public List<string> Books { get; set; }
        public string? SyllabusId { get; set; } = null; //foreign key
        [JsonIgnore]
        public Syllabus? Syllabus { get; set; } = null; //one-to-one
        public List<Unit> Units {  get; set; } //one-to-many
        public List<Assignment>? Assignments { get; set; } = null; //one-to-many
        [JsonIgnore]
        public List<Teacher> Teachers { get; set; } //one-to-many
        [JsonIgnore]
        public List<Student>? Students { get; set; } = null; //one-to-many
    }
}
