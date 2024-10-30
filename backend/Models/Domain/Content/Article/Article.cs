namespace backend.Models.Domain.Content.Article
{
    public class Article
    {
        public string ArticleId { get; set; }
        public string Headline { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; } //foreign key
        public Author Author { get; set; } // one-to-one
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
