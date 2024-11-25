using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.DTO.Content.Syllabus;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyllabusController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISyllabusRepository syllabusRepository;

        public SyllabusController(IMapper mapper, ISyllabusRepository syllabusRepository)
        {
            this.mapper = mapper;
            this.syllabusRepository = syllabusRepository;
        }
        [HttpPost("CreateCourse")]
        [ValidateModel]
        public async Task<IActionResult> CreateCourse([FromBody] AddCourseDTO addCourseDTO)
        {
            var course = mapper.Map<Course>(addCourseDTO);

            course = await syllabusRepository.CreateCourse(course);

            if (course == null) { return BadRequest("Adding course failed."); }
            else { return Ok(mapper.Map<CourseDTO>(course)); }
        }
        [HttpGet("GetCourse")]
        [ValidateModel]
        public async Task<IActionResult> GetCourse()
        {
            var courses = await syllabusRepository.GetCourse();
            if (courses == null) { return BadRequest("No courses found."); }
            else { return Ok(mapper.Map<List<CourseDTO>>(courses)); }
        }
        [HttpGet("GetCourseById/{CourseId}")]
        [ValidateModel]
        public async Task<IActionResult> GetCourseById([FromRoute] string CourseId)
        {
            var course = await syllabusRepository.GetCourseById(CourseId);
            if (course == null) { return BadRequest("No course found."); }
            else { return Ok(mapper.Map<CourseDTO>(course)); }
        }
        [HttpPut("UpdateCourse/{CourseId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCourse([FromRoute] string CourseId,
            [FromBody] UpdateCourseDTO updateCourseDTO)
        {
            var course = await syllabusRepository.UpdateCourse(CourseId, mapper.Map<Course>(updateCourseDTO));
            if (course == null) { return BadRequest("No course found."); }
            else { return Ok(mapper.Map<CourseDTO>(course)); }
        }
        [HttpDelete("DeleteCourse/{CourseId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteCourse([FromRoute] string CourseId)
        {
            var course = await syllabusRepository.DeleteCourse(CourseId);
            if (course == null) { return BadRequest("No course found."); }
            else { return Ok(mapper.Map<CourseDTO>(course)); }
        }
        [HttpPost("CreateSyllabus")]
        [ValidateModel]
        public async Task<IActionResult> CreateSyllabus([FromBody] AddSyllabusDTO addSyllabusDTO)
        {
            var syllabus = await syllabusRepository.CreateSyllabus(mapper.Map<Syllabus>(addSyllabusDTO),addSyllabusDTO);
            if (syllabus == null) { return BadRequest("Syllabus couldn't be created."); }
            else
            {
                return Ok(mapper.Map<SyllabusDTO>(syllabus));
            }
        }
        [HttpGet("GetSyllabus")]
        [ValidateModel]
        public async Task<IActionResult> GetSyllabus()
        {
            var syllabus = await syllabusRepository.GetSyllabus();
            if (syllabus == null) { return BadRequest("No syllabus found."); }
            else
            {
                return Ok(mapper.Map<List<SyllabusDTO>>(syllabus));
            }
        }
        [HttpGet("GetSyllabusById/{SyllabusId}")]
        [ValidateModel]
        public async Task<IActionResult> GetSyllabusById([FromRoute]string SyllabusId)
        {
            var syllabus = await syllabusRepository.GetSyllabusById(SyllabusId);
            if (syllabus == null) { return BadRequest("No syllabus found."); }
            else
            {
                return Ok(mapper.Map<SyllabusDTO>(syllabus));
            }
        }
        [HttpPut("UpdateSyllabus/{SyllabusId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateSyllabus([FromRoute] string SyllabusId,
            [FromBody]UpdateSyllabusDTO updateSyllabusDTO)
        {
            var syllabus = await syllabusRepository.UpdateSyllabus(SyllabusId,
                mapper.Map<Syllabus>(updateSyllabusDTO),
                updateSyllabusDTO);
            if (syllabus == null) { return BadRequest("Syllabus couldn't be updated."); }
            else
            {
                return Ok(mapper.Map<SyllabusDTO>(syllabus));
            }
        }
        [HttpDelete("DeleteSyllabus/{SyllabusId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteSyllabus([FromRoute] string SyllabusId)
        {
            var syllabus = await syllabusRepository.DeleteSyllabus(SyllabusId);
            if (syllabus == null) { return BadRequest("Syllabus couldn't be deleted."); }
            else
            {
                return Ok(mapper.Map<SyllabusDTO>(syllabus));
            }
        }
    }
}
