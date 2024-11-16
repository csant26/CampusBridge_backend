using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Assignment
{
    public class AddSubmissionDTO
    {
        [Required]
        public string SubmissionId { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string AssignmentId { get; set; }
    }
}
