using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Universities;
using backend.Models.DTO.University;
using backend.Repository.Universities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUniversityRepository universityRepository;

        public UniversityController(IMapper mapper, IUniversityRepository universityRepository)
        {
            this.mapper = mapper;
            this.universityRepository = universityRepository;
        }
        [HttpPost("CreateUniversity")]
        [ValidateModel]
        public async Task<IActionResult> CreateUniversity([FromBody] AddUniversityDTO addUniversityDTO)
        {
            var university = await universityRepository
                .CreateUniversity(mapper.Map<University>(addUniversityDTO));
            if (university == null) { return BadRequest("University couldn't be created."); }
            return Ok(mapper.Map<UniversityDTO>(university));
        }
        [HttpGet("GetUniversity")]
        [ValidateModel]
        public async Task<IActionResult> GetUniversity()
        {
            var universities = await universityRepository.GetUniversity();
            if (universities == null) { return BadRequest("No universities found."); }
            return Ok(mapper.Map<List<UniversityDTO>>(universities));
        }
        [HttpGet("GetUniversityById/{UniversityId}")]
        [ValidateModel]
        public async Task<IActionResult> GetUniversityById([FromRoute] string UniversityId)
        {
            var university = await universityRepository.GetUniversityById(UniversityId);
            if (university == null) { return BadRequest("No university found."); }
            return Ok(mapper.Map<UniversityDTO>(university));
        }
        [HttpPut("UpdateUniversity/{UniversityId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUniversity([FromRoute]string UniversityId,
            [FromBody]UpdateUniversityDTO updateUniversityDTO)
        {
            var university = await universityRepository.UpdateUniversity(UniversityId,
                mapper.Map<University>(updateUniversityDTO));
            if (university == null) { return BadRequest("University couldn't be updated."); }
            return Ok(mapper.Map<UniversityDTO>(university));
        }
        [HttpDelete("DeleteUniversity/{UniversityId}/{DeveloperId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteUniversity([FromRoute] string UniversityId,
            [FromRoute]string DeveloperId)
        {
            var university = await universityRepository.DeleteUniversity(UniversityId,DeveloperId);
            if (university == null) { return BadRequest("University couldn't be deleted."); }
            return Ok(mapper.Map<UniversityDTO>(university));
        }
    }
}
