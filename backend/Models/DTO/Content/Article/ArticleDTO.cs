using backend.Models.Domain.Content;
using System;

namespace backend.Models.DTO.Content.Article
{
    public class ArticleDTO
    {
        public string ArticleId { get; set; }
        public string Headline { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public AuthorDTO AuthorDTO { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated {  get; set; }
    }
}
