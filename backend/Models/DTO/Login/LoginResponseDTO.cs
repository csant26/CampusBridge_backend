namespace backend.Models.DTO.Login
{
    public class LoginResponseDTO
    {
        public string jwtToken { get; set; }
        public string role { get; set; }
    }
}
