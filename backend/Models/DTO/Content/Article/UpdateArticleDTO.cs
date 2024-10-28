namespace backend.Models.DTO.Content.Article
{
    public class UpdateArticleDTO
    {
        public string Headline { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
