using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Student;
using backend.Models.DTO.Student;
using backend.Repository.College;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "UniversityAdmin")]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IStudentRepository studentRepository;

        public StudentController(IMapper mapper, IStudentRepository studentRepository)
        {
            this.mapper = mapper;
            this.studentRepository = studentRepository;
        }
        [HttpPost("CreateStudent")]
        [ValidateModel]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentDTO addStudentDTO)
        {
            //Map DTO to Domain. (only maps some of the properties)
            Student student = mapper.Map<Student>(addStudentDTO);

            //Map the remaining relational properties manually.
            //And, finally use the domain to access the database through repository pattern.
            student = await studentRepository.CreateStudent(student,addStudentDTO);

            //Map the domain to DTO to pass back to the user.
            return Ok(mapper.Map<StudentDTO>(student));
        }
        [HttpGet("GetStudent")]
        [ValidateModel]
        public async Task<IActionResult> GetStudent()
        {
            var students = await studentRepository.GetStudent();
            //return Ok(students);
            return Ok(mapper.Map<List<StudentDTO>>(students));
        }
        [HttpGet("GetStudentById/{id}")]
        [ValidateModel]
        //[Route("{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] string id)
        {
            return Ok(mapper.Map<StudentDTO>(await studentRepository.GetStudentById(id)));
        }
        [HttpPut("UpdateStudent/{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateStudent([FromRoute] string id,
            [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            var updatedStudent = mapper.Map<Student>(updateStudentDTO);

            updatedStudent = await studentRepository.UpdateStudent(id, updatedStudent, updateStudentDTO);

            return Ok(mapper.Map<StudentDTO>(updatedStudent));
        }
        [HttpDelete("DeleteStudent/{id}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteStudent([FromRoute] string id)
        {
            return Ok(mapper.Map<StudentDTO>(await studentRepository.DeleteStudent(id)));
        }
    }
}
