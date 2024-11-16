using backend.Models.Domain.Content;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Article
{
    public class AddArticleDTO
    {
        [Required]
        [MaxLength(20)]
        public string ArticleId { get; set; }
        [Required]
        [MinLength(3)]
        public string Headline { get; set; }
        [Required]
        public string Tagline { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
    }
}
