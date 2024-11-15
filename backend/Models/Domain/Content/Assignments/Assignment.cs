using backend.Models.Domain.Content.Images;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Assignments
{
    public class Assignment
    {
        public string AssignmentId { get; set; }
        public string Question { get; set; }
        public List<Image>? QuestionImage { get; set; } = null;
        public string CourseId { get; set; } //foreign key
        [JsonIgnore]
        public Course Course { get; set; } //one-to-one
        public DateTime AssignedDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string TeacherId { get; set; } //foreign key
        [JsonIgnore]
        public Teacher Teacher { get; set; } //one-to-one
        public List<Submission>? Submissions { get; set; } = null;
    }
}
