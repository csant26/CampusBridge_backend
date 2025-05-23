﻿using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Articles;
using backend.Models.DTO.Content.Article;
using backend.Repository.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;

        public ArticleController(IArticleRepository articleRepository,
            IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateArticle/{creatorId}")]
        [ValidateModel]
        public async Task<IActionResult> CreateArticle([FromRoute]string creatorId, 
            [FromBody] AddArticleDTO addarticleDTO)
        {
            var article = mapper.Map<Article>(addarticleDTO);
            article = await articleRepository.CreateArticle(creatorId, article);
            if (article == null) { return BadRequest("Unable to create article."); }
            return Ok(mapper.Map<ArticleDTO>(article));
        }
        [HttpGet("GetArticle")]
        [ValidateModel]
        public async Task<IActionResult> GetArticle()
        {
            var articles = await articleRepository.GetArticle();
            if(articles == null) { return BadRequest("Unable to fetch articles."); };
            return Ok(mapper.Map<List<ArticleDTO>>(articles));
        }
        [HttpGet("GetArticleById/{articleId}")]
        [ValidateModel]
        public async Task<IActionResult> GetArticleById([FromRoute] string articleId)
        {
            var article = await articleRepository.GetArticleById(articleId);
            if(article == null) { return BadRequest("Unable to fetch article."); };
            return Ok(mapper.Map<ArticleDTO>(article));
        }
        [HttpPut("UpdateArticle/{articleId}/{creatorId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateArticle([FromRoute] string articleId,
            [FromRoute] string creatorId,
            [FromBody]UpdateArticleDTO updateArticleDTO)
        {
            var updatedArticle = mapper.Map<Article>(updateArticleDTO);
            updatedArticle = await articleRepository.UpdateArticle(creatorId,articleId,updatedArticle);
            if (updatedArticle == null) { return BadRequest("Unable to update article."); };
            return Ok(mapper.Map<ArticleDTO>(updatedArticle));
        }
        [HttpDelete("DeleteArticle/{articleId}/{creatorId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteArticle([FromRoute] string articleId,
            [FromRoute] string creatorId)
        {
            var exisitingArticle = await articleRepository.DeleteArticle(creatorId, articleId);
            if (exisitingArticle == null) { return BadRequest("Unable to delete article."); };
            return Ok(mapper.Map<ArticleDTO>(exisitingArticle));
        }

    }
}