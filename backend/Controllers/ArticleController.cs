using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Article;
using backend.Models.DTO.Content.Article;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        [ValidateModel]
        [Route("{id}")]
        public async Task<IActionResult> CreateArticle([FromRoute]string id, 
            [FromBody] AddArticleDTO addarticleDTO)
        {
            var article = mapper.Map<Article>(addarticleDTO);
            article = await articleRepository.CreateArticle(id, article);
            return Ok(mapper.Map<ArticleDTO>(article));
        }
    }
}
