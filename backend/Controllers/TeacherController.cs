using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Colleges;
using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using backend.Models.DTO.College;
using backend.Models.DTO.Student;
using backend.Models.DTO.Teacher;
using backend.Repository.Colleges;
using backend.Repository.Students;
using backend.Repository.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITeacherRepository teacherRepository;

        public TeacherController(IMapper mapper, ITeacherRepository teacherRepository)
        {
            this.mapper = mapper;
            this.teacherRepository = teacherRepository;
        }
        [HttpPost("CreateTeacher")]
        [ValidateModel]
        public async Task<IActionResult> CreateTeacher([FromBody] AddTeacherDTO addTeacherDTO)
        {
            var teacher = await teacherRepository
                .CreateTeacher(mapper.Map<Teacher>(addTeacherDTO),addTeacherDTO);
            if (teacher == null) { return BadRequest("Teacher couldn't be created."); }
            return Ok(mapper.Map<TeacherDTO>(teacher));
        }
        [HttpGet("GetTeacher")]
        [ValidateModel]
        public async Task<IActionResult> GetTeacher()
        {
            var teachers = await teacherRepository.GetTeacher();
            if (teachers == null) { return BadRequest("No teachers found."); }
            return Ok(mapper.Map<List<TeacherDTO>>(teachers));
        }
        [HttpGet("GetTeacherById/{TeacherId}")]
        [ValidateModel]
        public async Task<IActionResult> GetTeacherById([FromRoute] string TeacherId)
        {
            var teacher = await teacherRepository.GetTeacherById(TeacherId);
            if (teacher == null) { return BadRequest("No teacher found."); }
            return Ok(mapper.Map<TeacherDTO>(teacher));
        }
        [HttpGet("GetTeacherBySemeseter/{Semester}")]
        [ValidateModel]
        public async Task<IActionResult> GetTeacherBySemester([FromRoute] string Semester)
        {
            var teacher = await teacherRepository.GetTeacherBySemester(Semester);
            if (teacher == null) { return BadRequest("No teacher found."); }
            return Ok(teacher);
        }
        [HttpPut("UpdateTeacher/{TeacherId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateTeacher([FromRoute] string TeacherId,
            [FromBody] UpdateTeacherDTO updateTeacherDTO)
        {
            var teacher = await teacherRepository.UpdateTeacher(TeacherId,
                mapper.Map<Teacher>(updateTeacherDTO),updateTeacherDTO);
            if (teacher == null) { return BadRequest("Teacher couldn't be updated."); }
            return Ok(mapper.Map<TeacherDTO>(teacher));
        }
        [HttpDelete("DeleteTeacher/{TeacherId}/{CollegeId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteTeacher([FromRoute] string TeacherId,
            [FromRoute] string CollegeId)
        {
            var teacher = await teacherRepository.DeleteTeacher(TeacherId,CollegeId);
            if (teacher == null) { return BadRequest("Teacher couldn't be deleted."); }
            return Ok(mapper.Map<TeacherDTO>(teacher));
        }
    }
}
