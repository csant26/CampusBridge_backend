using AutoMapper;
using backend.CustomActionFilter;
using backend.Data;
using backend.Models.Domain.Content.Schedules;
using backend.Models.DTO.Content.Schedule;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMapper mapper;

        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateSchedule")]
        [ValidateModel]
        public async Task<IActionResult> CreateScheulde([FromBody] AddScheduleDTO addScheduleDTO)
        {
            var schedule = await scheduleRepository
                .CreateSchedule(mapper.Map<Schedule>(addScheduleDTO));
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(mapper.Map<ScheduleDTO>(schedule));
        }
        [HttpGet("GetScheduleByRole")]
        [ValidateModel]
        public async Task<IActionResult> GetScheduleByRole(string Role)
        {
            var schedules = await scheduleRepository.GetScheduleByRole(Role);
            if (schedules == null) { return BadRequest("No schedules found."); }
            return Ok(mapper.Map<List<ScheduleDTO>>(schedules));
        }
        [HttpPost("CreateExamSchedule")]
        [ValidateModel]
        public async Task<IActionResult> CreateExamSchedule([FromBody] AddExamSchedule addExamSchedule)
        {
            var schedule = await scheduleRepository
                .CreateExamSchedule(mapper.Map<ExamSchedule>(addExamSchedule));
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(mapper.Map<ScheduleDTO>(schedule));
        }
        [HttpPost("CreateTeacherSchedule")]
        [ValidateModel]
        public async Task<IActionResult> CreateTeacherSchedule([FromBody] AddScheduleDTO addScheduleDTO)
        {
            var schedule = await scheduleRepository
                .CreateSchedule(mapper.Map<Schedule>(addScheduleDTO));
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(mapper.Map<ScheduleDTO>(schedule));
        }
        [HttpPost("CreateStudentSchedule")]
        [ValidateModel]
        public async Task<IActionResult> CreateStudentSchedule([FromBody] AddScheduleDTO addScheduleDTO)
        {
            var schedule = await scheduleRepository
                .CreateSchedule(mapper.Map<Schedule>(addScheduleDTO));
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(mapper.Map<ScheduleDTO>(schedule));
        }
    }
}