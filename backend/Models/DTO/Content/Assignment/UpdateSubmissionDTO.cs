using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Assignment
{
    public class UpdateSubmissionDTO
    {
        [Required]
        public string Answer { get; set; }
    }
}
