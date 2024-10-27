using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Student;
using backend.Models.DTO.Student;
using backend.Repository.College;
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
        [HttpPost]
        [ValidateModel]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentRequestDTO addStudentDTO)
        {
            //Map DTO to Domain.
            Student student = mapper.Map<Student>(addStudentDTO);

            //Use the domain to access database through repository pattern.
            student = await collegeRepository.CreateStudent(student);

            //Map the domain to DTO to pass back to the user.
            return Ok(mapper.Map<StudentDTO>(student));
        }
        [HttpGet]
        [ValidateModel]
        [Route("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            var students = await collegeRepository.GetStudent();
            return Ok(mapper.Map<List<StudentDTO>>(students));
        }

    }
}
