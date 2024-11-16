using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Teachers;
using backend.Models.DTO.Content.Syllabus;
using backend.Models.DTO.Teacher;

namespace backend.Models.DTO.Content.Assignment
{
    public class AddAssignmentDTO
    {
        public string AssignmentId { get; set; }
        public string Question { get; set; }
        public string CourseId {  get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string? TeacherId { get; set; }
    }
}
