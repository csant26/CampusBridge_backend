using backend.Models.Domain.Content.Articles;
using backend.Models.DTO.Content.Article;

namespace backend.Repository.Content
{
    public interface IArticleRepository
    {
        Task<Article> CreateArticle(string creatorId, Article article);
        Task<List<Article>> GetArticle();
        Task<Article> GetArticleById(string id);
        Task<Article> UpdateArticle(string creatorId, string articleId, Article updatedArticle);
        Task<Article> DeleteArticle(string creatorId, string articleId);
    }
}
