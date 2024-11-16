using backend.Models.Domain.Content.Help;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Help
{
    public class AddAnswerDTO
    {
        public string AnswerId { get; set; }
        public string AnswerDetails { get; set; }
        public string AnswerBy { get; set; }
        public string QuestionId {  get; set; }
    }
}
