using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Results;
using backend.Models.DTO.Content.Result;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository resultRepository;
        private readonly IMapper mapper;

        public ResultController(IResultRepository resultRepository, IMapper mapper)
        {
            this.resultRepository = resultRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateResult")]
        [ValidateModel]
        public async Task<IActionResult> CreateResult([FromBody]AddResultDTO addResultDTO)
        {
            var result = await resultRepository.CreateResult(mapper.Map<Result>(addResultDTO));
            if (result == null) { return BadRequest("Result couldn't be created."); }
            return Ok(result);
        }
        [HttpGet("GetResult")]
        [ValidateModel]
        public async Task<IActionResult> GetResult()
        {
            var results = await resultRepository.GetResult();
            if (results == null) { return BadRequest("No results were found."); }
            return Ok(mapper.Map<List<ResultDTO>>(results));
        }
        [HttpGet("GetResultById/{ResultId}")]
        [ValidateModel]
        public async Task<IActionResult> GetResult([FromRoute]string ResultId)
        {
            var result = await resultRepository.GetResultById(ResultId);
            if (result == null) { return BadRequest("No results were found."); }
            return Ok(mapper.Map<ResultDTO>(result));
        }
        [HttpPut("UpdateResult/{ResultId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateResult([FromRoute] string ResultId,
            [FromBody]UpdateResultDTO updateResultDTO)
        {
            var result = await resultRepository.UpdateResult(ResultId, mapper.Map<Result>(updateResultDTO));
            if (result == null) { return BadRequest("Result couldn't be updated."); }
            return Ok(mapper.Map<ResultDTO>(result));
        }
        [HttpPut("DeleteResult/{ResultId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteResult([FromRoute] string ResultId)
        {
            var result = await resultRepository.DeleteResult(ResultId);
            if (result == null) { return BadRequest("Result couldn't be delete."); }
            return Ok(mapper.Map<ResultDTO>(result));
        }
    }
}
