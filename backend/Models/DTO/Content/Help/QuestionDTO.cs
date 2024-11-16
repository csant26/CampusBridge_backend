using backend.Models.Domain.Content.Help;
using backend.Models.DTO.Student;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Help
{
    public class QuestionDTO
    {
        public string QuestionId { get; set; }
        public string QuestionDetails { get; set; }
        public List<string> DirectedTo { get; set; }
        public StudentDTO StudentDTO { get; set; }
        public List<AnswerDTO>? AnswerDTO { get; set; } = null;
    }
}
