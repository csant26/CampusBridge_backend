using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Assignment
{
    public class AddSubmissionDTO
    {
        public string SubmissionId { get; set; }
        public string Answer { get; set; }
        public string? ImagePath { get; set; } = null;
        public string StudentId { get; set; }
        public string AssignmentId { get; set; }
    }
}
