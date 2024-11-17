using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.University
{
    public class UpdateUniversityDTO
    {
        [Required]
        public string UniversityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string DeveloperId { get; set; }
    }
}
