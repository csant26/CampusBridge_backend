using AutoMapper;
using backend.Models.Domain.Content.Attendances;
using backend.Models.DTO.Content.Attendances;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AttendanceRepository attendanceRepository;

        public AttendanceController(IMapper mapper, AttendanceRepository attendanceRepository)
        {
            this.mapper = mapper;
            this.attendanceRepository = attendanceRepository;
        }
        [HttpPost("CreateAttendance")]
        public async Task<IActionResult> CreateAttendance([FromBody]AddAttendanceDTO addAttendanceDTO)
        {
            var attendance = await attendanceRepository.CreateAttendance(mapper.Map<Attendance>(addAttendanceDTO),
                addAttendanceDTO);
            if (attendance == null) { return BadRequest("Couldn't create attendance."); }
            return Ok(mapper.Map<AttendanceDTO>(attendance));
        }
        [HttpGet("GetAttendance")]
        public async Task<IActionResult> GetAttendance()
        {
            var attendance = await attendanceRepository.GetAttendance();
            if (attendance == null) { return BadRequest("No attendance found."); }
            return Ok(mapper.Map<List<AttendanceDTO>>(attendance));
        }
    }
}
