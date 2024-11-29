using System;

namespace backend.Models.Domain.Content.Articles
{
    public class Article
    {
        public string ArticleId { get; set; }
        public string Headline { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; } 
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }   

    }
}
