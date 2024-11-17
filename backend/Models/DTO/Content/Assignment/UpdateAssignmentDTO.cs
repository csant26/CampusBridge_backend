using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Assignment
{
    public class UpdateAssignmentDTO
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string CourseId { get; set; }
        [Required]
        public DateTime AssignedDate { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        [Required]
        public string TeacherId { get; set; } //Submitted for validation
    }
}
