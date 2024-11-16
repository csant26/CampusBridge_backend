using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Help
{
    public class Answer
    {
        public string AnswerId { get; set; }
        public string AnswerDetails {  get; set; }
        public string AnswerBy {  get; set; }
        public string QuestionId { get; set; } //foreign key
        [JsonIgnore]
        public Question Question { get; set; } //one-to-one
    }
}
