using backend.Models.Domain.Students;
using backend.Models.DTO.Student;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Assignment
{
    public class SubmissionDTO
    {
        public string SubmissionId { get; set; }
        public string Answer { get; set; }
        public string? FilePath { get; set; } = null;
        public string? Score { get; set; }
        public StudentDTO StudentDTO { get; set; } //one-to-one
        public AssignmentDTO AssignmentDTO { get; set; } //one-to-one
    }
}
