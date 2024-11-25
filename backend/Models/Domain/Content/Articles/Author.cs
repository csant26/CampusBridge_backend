using System.Collections.Generic;

namespace backend.Models.Domain.Content.Articles
{
    public class Author
    {
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public string AuthorType { get; set; }
        public string CampusId { get; set; }
        public List<Article> Articles { get; set; } //one-to-many
    }
}
