using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Student
{
    public class UpdateStudentDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        [Required]
        public string Semseter { get; set; }
        public string isClubHead { get; set; }
        public string isAuthor { get; set; }
        public List<string>? ElectiveIds { get; set; } = null;
        public string FinancialId { get; set; }
        public List<string>? ClubIds { get; set; } = null ;
        public string CreatorId { get; set; } //Creator: College or Student
    }
}
