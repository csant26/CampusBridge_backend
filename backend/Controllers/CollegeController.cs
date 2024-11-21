using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Colleges;
using backend.Models.Domain.Universities;
using backend.Models.DTO.College;
using backend.Models.DTO.University;
using backend.Repository.Colleges;
using backend.Repository.Universities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICollegeRepository collegeRepository;

        public CollegeController(IMapper mapper, ICollegeRepository collegeRepository)
        {
            this.mapper = mapper;
            this.collegeRepository = collegeRepository;
        }
        [HttpPost("CreateCollege")]
        [ValidateModel]
        public async Task<IActionResult> CreateCollege([FromBody] AddCollegeDTO addCollegeDTO)
        {
            var college = await collegeRepository
                .CreateCollege(mapper.Map<College>(addCollegeDTO));
            if (college == null) { return BadRequest("College couldn't be created."); }
            return Ok(mapper.Map<CollegeDTO>(college));
        }
        [HttpGet("GetCollege")]
        [ValidateModel]
        public async Task<IActionResult> GetCollege()
        {
            var colleges = await collegeRepository.GetCollege();
            if (colleges == null) { return BadRequest("No colleges found."); }
            return Ok(mapper.Map<List<CollegeDTO>>(colleges));
        }
        [HttpGet("GetCollegeById/{CollegeId}")]
        [ValidateModel]
        public async Task<IActionResult> GetCollegeById([FromRoute] string CollegeId)
        {
            var college = await collegeRepository.GetCollegeById(CollegeId);
            if (college == null) { return BadRequest("No college found."); }
            return Ok(mapper.Map<CollegeDTO>(college));
        }
        [HttpPost("UpdateCollege/{CollegeId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCollege([FromRoute] string CollegeId,
            [FromBody] UpdateCollegeDTO updateCollegeDTO)
        {
            var college = await collegeRepository.UpdateCollege(CollegeId,
                mapper.Map<College>(updateCollegeDTO));
            if (college == null) { return BadRequest("College couldn't be updated."); }
            return Ok(mapper.Map<CollegeDTO>(college));
        }
        [HttpPost("DeleteCollege/{CollegeId}/{UniversityId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteCollege([FromRoute] string CollegeId,
            [FromRoute] string UniversityId)
        {
            var college = await collegeRepository.DeleteCollege(CollegeId, UniversityId);
            if (college == null) { return BadRequest("College couldn't be deleted."); }
            return Ok(mapper.Map<CollegeDTO>(college));
        }
    }
}
