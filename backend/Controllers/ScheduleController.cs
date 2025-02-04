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
        private readonly ITeacherScheduleRepository teacherScheduleRepository;

        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper,
            ITeacherScheduleRepository teacherScheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
            this.teacherScheduleRepository = teacherScheduleRepository;
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
        //[HttpPost("CreateTeacherSchedule")]
        //[ValidateModel] 
        //public async Task<IActionResult> CreateTeacherSchedule([FromBody] AddTeacherScheduleDTO addTeacherScheduleDTO)
        //{
        //    var schedule = await scheduleRepository
        //        .CreateTeacherSchedule(mapper.Map<TeacherSchedule>(addTeacherScheduleDTO));
        //    if (schedule == null) { return BadRequest("Schedule can't be created."); }
        //    return Ok(mapper.Map<ScheduleDTO>(schedule));
        //}
        [HttpPost("CreateStudentSchedule")]
        [ValidateModel]
        public async Task<IActionResult> CreateStudentSchedule([FromBody] AddScheduleDTO addScheduleDTO)
        {
            var schedule = await scheduleRepository
                .CreateSchedule(mapper.Map<Schedule>(addScheduleDTO));
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(mapper.Map<ScheduleDTO>(schedule));
        }
        [HttpPost("CreateTeacherScheduleFromGraph")]
        public async Task<IActionResult> CreateTeacherScheduleFromGraph(List<ClassSession> sessions)
        {
            var schedule = await teacherScheduleRepository.CreateTeacherScheduleFromGraph(sessions);
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(schedule) ;
        }

    }
}