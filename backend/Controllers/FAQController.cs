using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.DTO.Content.FAQ;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFAQRepository fAQRepository;
        private readonly IMapper mapper;

        public FAQController(IFAQRepository fAQRepository,IMapper mapper)
        {
            this.fAQRepository = fAQRepository;
            this.mapper = mapper;
        }
        [HttpPost("GetAnswer")]
        [ValidateModel]
        public async Task<IActionResult> GetAnswer([FromBody] FAQRequestDTO fAQRequestDTO)
        {
            return Ok(await fAQRepository.GetAnswer(fAQRequestDTO));
        }
    }
}
