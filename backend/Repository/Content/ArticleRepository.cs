using backend.Data;
using backend.Models.Domain.Content.Articles;
using backend.Models.DTO.Content.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public ArticleRepository(CampusBridgeDbContext campusBridgeDbContext, UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<Article> CreateArticle(string creatorId, Article article)
        {

            var authorUser = await userManager.FindByEmailAsync(creatorId);
            if (authorUser == null) { return null; }
            article.AuthorId = authorUser.Email;
            article.DateUpdated = article.DatePosted;
            await campusBridgeDbContext.Articles.AddAsync(article);
            await campusBridgeDbContext.SaveChangesAsync();
            return article;
        }
        public async Task<List<Article>> GetArticle()
        {
            var articles = await campusBridgeDbContext.Articles
                .ToListAsync();
            if(articles != null) { return articles; }
            else { return null; }
        }

        public async Task<Article> GetArticleById(string id)
        {
            var article = await campusBridgeDbContext.Articles
                .FirstOrDefaultAsync(x => x.ArticleId == id);
            if (article != null) { return article; }
            else { return null; }
        }

        public async Task<Article> UpdateArticle(string creatorId, string articleId, Article updatedArticle)
        {
            var existingArticle = await GetArticleById(articleId);
            if (existingArticle != null)
            {
                if(existingArticle.AuthorId == creatorId)
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
                if(existingArticle.AuthorId == creatorId)
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
