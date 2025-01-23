using backend.CustomActionFilter;
using backend.Repository.Analysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsRepository analyticsRepository;

        public AnalyticsController(IAnalyticsRepository analyticsRepository)
        {
            this.analyticsRepository = analyticsRepository;
        }
        [HttpPost("GetGenderData")]
        [ValidateModel]
        public async Task<IActionResult> GetGenderData()
        {
            return Ok(await analyticsRepository.GetGenderData());
        }
        [HttpPost("GetResultData")]
        [ValidateModel]
        public async Task<IActionResult> GetResultData()
        {
            return Ok(await analyticsRepository.GetResultData());
        }
        [HttpPost("GetGraduateData")]
        [ValidateModel]
        public async Task<IActionResult> GetGraduateData()
        {
            return Ok(await analyticsRepository.GetGraduateData());
        }
    }
}
