using AutoMapper;
using backend.Models.Domain.Content.Syllabus;
using backend.Models.DTO.Content.Syllabus;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] AddCourseDTO addCourseDTO)
        {
            var course = mapper.Map<Course>(addCourseDTO);

            course = await syllabusRepository.CreateCourse(course);

            if(course== null) { return BadRequest("Adding course failed."); }
            else { return Ok(mapper.Map<CourseDTO>(course)); }
        }
    }
}
