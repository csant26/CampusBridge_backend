using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Notices;
using backend.Models.DTO.Content.Notice;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeController : ControllerBase
    {
        private readonly INoticeRepository noticeRepository;
        private readonly IMapper mapper;

        public NoticeController(INoticeRepository noticeRepository, IMapper mapper)
        {
            this.noticeRepository = noticeRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateNotice/{CreatorId}")]
        [ValidateModel]
        public async Task<IActionResult> CreateNotice([FromBody] AddNoticeDTO addNoticeDTO)
        {
            var notice = await noticeRepository.CreateNotice(mapper.Map<Notice>(addNoticeDTO));
            if (notice == null) { return BadRequest("Notice couldn't be created."); }
            return Ok(mapper.Map<NoticeDTO>(notice));
        }
        [HttpGet("GetNotice")]
        [ValidateModel]
        public async Task<IActionResult> GetNotice()
        {
            var notices = await noticeRepository.GetNotice();
            if (notices == null) { return BadRequest("No notices found."); }
            return Ok(mapper.Map<List<NoticeDTO>>(notices));
        }
        [HttpGet("GetNoticeById/{NoticeId}")]
        [ValidateModel]
        public async Task<IActionResult> GetNoticeById([FromRoute] string NoticeId)
        {
            var notice = await noticeRepository.GetNoticeById(NoticeId);
            if (notice == null) { return BadRequest("No notice found."); }
            return Ok(mapper.Map<NoticeDTO>(notice));
        }
        [HttpGet("GetNoticeByCreator/{Creator}")]
        [ValidateModel]
        public async Task<IActionResult> GetNoticeByRole([FromRoute] string Creator)
        {
            var notices = await noticeRepository.GetNoticeByCreator(Creator);
            if (notices == null) { return BadRequest("No notice found."); }
            return Ok(mapper.Map<List<NoticeDTO>>(notices));
        }
        [HttpGet("GetNoticeByAudience/{Audience}")]
        [ValidateModel]
        public async Task<IActionResult> GetNoticeByAudience([FromRoute] string Audience)
        {
            var notices = await noticeRepository.GetNoticeByAudience(Audience);
            if (notices == null) { return BadRequest("No notice found."); }
            return Ok(mapper.Map<List<NoticeDTO>>(notices));
        }
        [HttpPut("UpdateNotice/{NoticeId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateNotice([FromRoute] string NoticeId,
            [FromBody] UpdateNoticeDTO updateNoticeDTO)
        {
            var notice = await noticeRepository.UpdateNotice(NoticeId,
                mapper.Map<Notice>(updateNoticeDTO));
            if (notice == null) { return BadRequest("Notice couldn't be updated."); }
            return Ok(mapper.Map<NoticeDTO>(notice));
        }
        [HttpDelete("DeleteNotice/{NoticeId}/{CreatorId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteNotice([FromRoute] string NoticeId,
            [FromRoute] string CreatorId)
        {
            var notice = await noticeRepository.DeleteNotice(NoticeId, CreatorId);
            if (notice == null) { return BadRequest("Notice couldn't be deleted."); }
            return Ok(mapper.Map<NoticeDTO>(notice));
        }
    }
}
