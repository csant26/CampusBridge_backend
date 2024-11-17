using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Help
{
    public class UpdateAnswerDTO
    {
        [Required]
        public string AnswerDetails { get; set; }
        [Required]
        public string AnswerById { get; set; } //Unchangeable but supplied for validation
    }
}
