using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Help
{
    public class UpdateQuestionDTO
    {
        [Required]
        public string QuestionDetails { get; set; }
        [Required]
        public List<string> DirectedTo { get; set; }
        [Required]
        public string StudentId { get; set; } // Unchangeable but supplied for validation

    }
}
