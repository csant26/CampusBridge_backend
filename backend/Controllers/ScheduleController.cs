using AutoMapper;
using backend.CustomActionFilter;
using backend.Data;
using backend.Models.Domain.Content.Schedules;
using backend.Models.DTO.Content.Schedule;
using backend.Repository.Content;
using backend.Repository.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMapper mapper;
        private readonly ITeacherScheduleRepository teacherScheduleRepository;
        private readonly ITeacherRepository teacherRepository;

        public ScheduleController(IScheduleRepository scheduleRepository, IMapper mapper,
            ITeacherScheduleRepository teacherScheduleRepository,
            ITeacherRepository teacherRepository)
        {
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
            this.teacherScheduleRepository = teacherScheduleRepository;
            this.teacherRepository = teacherRepository;
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
        [HttpGet("GetScheduleByTeacherId")]
        [ValidateModel]
        public async Task<IActionResult> GetScheduleByTeacherId(string Id)
            {
            var schedules = await scheduleRepository.GetScheduleByTeacherId(Id);
            if (schedules == null) { return BadRequest("No schedules found."); }
            return Ok(schedules);
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
        public async Task<IActionResult> CreateTeacherScheduleFromGraph(List<AddTeacherScheduleRequest> teacherScheduleRequest)
        {
            List<ClassSession> sessions = new List<ClassSession>();
            var teachers = await teacherRepository.GetCourseTeacherDataAsync();
            int index = 1;
            foreach(var teacher in teachers)
                {
                    var session = new ClassSession() { Id=index,CourseName = teacher.CourseTitle, TeacherId = teacher.TeacherId };
                    sessions.Add(session);
                    index++;
                }
            var schedule = await teacherScheduleRepository.CreateTeacherScheduleFromGraph(sessions, teacherScheduleRequest);
            if (schedule == null) { return BadRequest("Schedule can't be created."); }
            return Ok(schedule) ;
        }

    }
}