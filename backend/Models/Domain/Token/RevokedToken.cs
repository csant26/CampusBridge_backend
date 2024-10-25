namespace backend.Models.Domain.Token
{
    public class RevokedToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string RevokedAt { get; set; }
    }
}
