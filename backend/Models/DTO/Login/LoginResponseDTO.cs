namespace backend.Models.DTO.Login
{
    public class LoginResponseDTO
    {
        public string jwtToken { get; set; }
        public IList<string> role { get; set; }
        public string Id { get; set; }
    }
}
