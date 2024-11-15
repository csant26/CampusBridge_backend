using backend.Data;
using backend.Models.Domain.Content.Articles;
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
        public async Task<Article> CreateArticle(string creatorId, Article article)
        {
            var existingAuthor = await campusBridgeDbContext.Authors
                .FirstOrDefaultAsync(x => x.CampusId == creatorId);
            if(existingAuthor != null)
            {
                article.Author = existingAuthor;
                article.DateUpdated = article.DatePosted;
                await campusBridgeDbContext.Articles.AddAsync(article);
                await campusBridgeDbContext.SaveChangesAsync();
                return article;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<Article>> GetArticle()
        {
            var articles = await campusBridgeDbContext.Articles
                .Include(x=>x.Author)
                .ToListAsync();
            if(articles != null) { return articles; }
            else { return null; }
        }

        public async Task<Article> GetArticleById(string id)
        {
            var article = await campusBridgeDbContext.Articles
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.ArticleId == id);
            if (article != null) { return article; }
            else { return null; }
        }

        public async Task<Article> UpdateArticle(string creatorId, string articleId, Article updatedArticle)
        {
            var existingArticle = await GetArticleById(articleId);
            if (existingArticle != null)
            {
                if(existingArticle.Author.CampusId == creatorId)
                {
                    existingArticle.Headline = updatedArticle.Headline;
                    existingArticle.Tagline = updatedArticle.Tagline;
                    existingArticle.Description = updatedArticle.Description;
                    existingArticle.DateUpdated = updatedArticle.DateUpdated;
                    await campusBridgeDbContext.SaveChangesAsync();
                    return existingArticle;
                }
            }
            return null;
        }
        public async Task<Article> DeleteArticle(string creatorId, string articleId)
        {
            var existingArticle = await GetArticleById(articleId);
            if(existingArticle != null)
            {
                if(existingArticle.Author.CampusId == creatorId)
                {
                    campusBridgeDbContext.Remove(existingArticle);
                    await campusBridgeDbContext.SaveChangesAsync();
                    return existingArticle;
                }
            }
            return null;
        }
    }
}
