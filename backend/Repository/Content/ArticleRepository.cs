using backend.Data;
using backend.Models.Domain.Content.Article;
using backend.Models.DTO.Content.Article;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public ArticleRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Article> CreateArticle(string id, Article article)
        {
            var existingAuthor = await campusBridgeDbContext.Authors
                .FirstOrDefaultAsync(x => x.CampusId == id);
            if(existingAuthor != null)
            {
                article.Author = existingAuthor;
                await campusBridgeDbContext.Articles.AddAsync(article);
                await campusBridgeDbContext.SaveChangesAsync();
                return article;
            }
            else
            {
                return null;
            }
        }
    }
}
