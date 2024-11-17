using backend.Models.DTO.Student;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Help
{
    public class AddQuestionDTO
    {
        [Required]
        public string QuestionId { get; set; }
        [Required]
        public string QuestionDetails { get; set; }
        [Required]
        public List<string> DirectedTo { get; set; }
        [Required]
        public string StudentId { get; set; } //Supplied for validation
    }
}
