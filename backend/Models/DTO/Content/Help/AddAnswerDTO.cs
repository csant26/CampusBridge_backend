using backend.Models.Domain.Content.Help;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Help
{
    public class AddAnswerDTO
    {
        [Required]
        [MinLength(3)]
        public string AnswerId { get; set; }
        [Required]
        public string AnswerDetails { get; set; }
        [Required]
        public string AnswerById { get; set; } //Supplied for validation
        [Required]
        public string QuestionId {  get; set; } //Supplied for validation
    }
}
