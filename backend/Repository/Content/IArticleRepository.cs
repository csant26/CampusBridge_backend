using backend.Models.Domain.Content.Article;
using backend.Models.DTO.Content.Article;

namespace backend.Repository.Content
{
    public interface IArticleRepository
    {
        Task<Article> CreateArticle(string id, Article article);
    }
}
