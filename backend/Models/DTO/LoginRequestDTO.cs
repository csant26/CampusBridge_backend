using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string username {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
