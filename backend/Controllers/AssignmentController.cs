using AutoMapper;
using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IMapper mapper;

        public AssignmentController(IAssignmentRepository assignmentRepository,IMapper mapper)
        {
            this.assignmentRepository = assignmentRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateAssignment")]
        ///[Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAssignment([FromBody] AddAssignmentDTO addAssignmentDTO)
        {
            var assignment = assignmentRepository.CreateAssignment(mapper.Map<Assignment>(addAssignmentDTO));
            if (assignment == null) { return BadRequest("Assignment couldn't be created."); }
            else { return Ok(mapper.Map<AssignmentDTO>(assignment)); }

        }
        //[HttpGet("GetAssignment")]
        //public async Task<IActionResult> GetAssignment()
        //{

        //}
        //[HttpGet("GetAssignmentById/{AssignmentId}")]
        //public async Task<IActionResult> GetAssignmentById()
        //{

        //}
        //[HttpPut("UpdateAssignment/{AssignmentId}")]
        //public async Task<IActionResult> UpdateAssignment()
        //{

        //}
        //[HttpDelete("DeleteAssignment/{AssignmentId}")]
        //public async Task<IActionResult> DeleteAssignment()
        //{

        //}
        //[HttpPost("SubmitAssignment")]
        //public async Task<IActionResult> SubmitAssignment()
        //{

        //}
    }
}

