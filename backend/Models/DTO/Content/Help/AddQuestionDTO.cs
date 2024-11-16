using backend.Models.DTO.Student;

namespace backend.Models.DTO.Content.Help
{
    public class AddQuestionDTO
    {
        public string QuestionId { get; set; }
        public string QuestionDetails { get; set; }
        public List<string> DirectedTo { get; set; }
        public string StudentId { get; set; }
    }
}
