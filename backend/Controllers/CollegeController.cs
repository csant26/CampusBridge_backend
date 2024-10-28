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
        [Authorize(Roles = "UniversityAdmin")]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentDTO addStudentDTO)
        {
            //Map DTO to Domain. (only maps some of the properties)
            Student student = mapper.Map<Student>(addStudentDTO);

            //Map the remaining relational properties manually.
            //And, finally use the domain to access the database through repository pattern.
            student = await collegeRepository.CreateStudent(student,addStudentDTO);

            //Map the domain to DTO to pass back to the user.
            return Ok(mapper.Map<StudentDTO>(student));
        }
        [HttpGet]
        [ValidateModel]
        [Authorize(Roles = "UniversityAdmin")]
        public async Task<IActionResult> GetStudent()
        {
            var students = await collegeRepository.GetStudent();
            //return Ok(students);
            return Ok(mapper.Map<List<StudentDTO>>(students));
        }
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateStudent([FromRoute] string id,
            [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            var updatedStudent = mapper.Map<Student>(updateStudentDTO);

            updatedStudent = await collegeRepository.UpdateStudent(id, updatedStudent, updateStudentDTO);

            return Ok(mapper.Map<StudentDTO>(updatedStudent));
        }
        [HttpDelete("{id}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteStudent([FromRoute] string id)
        {
            return Ok(mapper.Map<StudentDTO>(await collegeRepository.DeleteStudent(id)));
        }
    }
}
