namespace backend.Models.Domain
{
    public class RevokedToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string RevokedAt { get; set; }
    }
}
