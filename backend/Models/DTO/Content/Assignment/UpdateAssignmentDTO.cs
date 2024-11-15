using backend.Models.DTO.Content.Images;

namespace backend.Models.DTO.Content.Assignment
{
    public class UpdateAssignmentDTO
    {
        public string Question { get; set; }
        public List<ImageUploadRequestDTO>? QuestionImage { get; set; } = null;
        public string CourseId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string TeacherId { get; set; }
    }
}
