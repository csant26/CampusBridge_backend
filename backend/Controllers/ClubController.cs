using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Students;
using backend.Models.DTO.Student;
using backend.Repository.Students;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IClubRepository clubRepository;

        public ClubController(IMapper mapper, IClubRepository clubRepository)
        {
            this.mapper = mapper;
            this.clubRepository = clubRepository;
        }
        [HttpPost("CreateClub")]
        [ValidateModel]
        public async Task<IActionResult> CreateClub([FromBody]AddClubDTO addClubDTO)
        {
            var club = await clubRepository.CreateClub(mapper.Map<Club>(addClubDTO));
            if (club == null) { return BadRequest("Club couldn't be created."); }
            return Ok(mapper.Map<ClubDTO>(club));
        }
        [HttpGet("GetClub")]
        [ValidateModel]
        public async Task<IActionResult> GetClub()
        {
            var clubs = await clubRepository.GetClub();
            if (clubs == null) { return BadRequest("No clubs found."); }
            return Ok(mapper.Map<List<ClubDTO>>(clubs));
        }
        [HttpGet("GetClubById/{ClubId}")]
        [ValidateModel]
        public async Task<IActionResult> GetClubById([FromRoute] string ClubId)
        {
            var club = await clubRepository.GetClubById(ClubId);
            if (club == null) { return BadRequest("No clubs found."); }
            return Ok(mapper.Map<ClubDTO>(club));
        }
        [HttpPut("UpdateClub/{ClubId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateClub([FromRoute] string ClubId,
            [FromBody]UpdateClubDTO updateClubDTO)
        {
            var club = await clubRepository.UpdateClub(ClubId, mapper.Map<Club>(updateClubDTO));
            if (club == null) { return BadRequest("Club couldn't be updated."); }
            return Ok(mapper.Map<ClubDTO>(club));
        }
        [HttpDelete("DeleteClub/{ClubId}/{ClubHeadId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteClub([FromRoute] string ClubId,
            [FromRoute] string ClubHeadId)
        {
            var club = await clubRepository.DeleteClub(ClubId, ClubHeadId);
            if (club == null) { return BadRequest("Club couldn't be deleted."); }
            return Ok(mapper.Map<ClubDTO>(club));
        }
    }
}
