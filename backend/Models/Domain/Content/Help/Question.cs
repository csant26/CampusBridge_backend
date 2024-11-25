using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Help
{
    public class Question
    {
        public string QuestionId { get; set; }
        public string QuestionDetails { get; set; }
        public List<string> DirectedTo { get; set; }
        public string StudentId { get; set; } //foreign key
        public Student Student { get; set; } //one-to-one  
        public List<Answer>? Answers { get; set; } = null;
    }
}
