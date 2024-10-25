namespace backend.Models.Domain
{
    public class AllToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt {  get; set; }
    }
}
