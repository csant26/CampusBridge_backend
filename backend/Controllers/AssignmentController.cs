using AutoMapper;
using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.File;
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
        public async Task<IActionResult> CreateAssignment([FromBody] AddAssignmentDTO addAssignmentDTO,
            [FromForm]FileUploadRequestDTO fileUploadRequestDTO)
        {
            var assignment = await assignmentRepository.CreateAssignment(
                mapper.Map<Assignment>(addAssignmentDTO),
                fileUploadRequestDTO);
            if (assignment == null) { return BadRequest("Assignment couldn't be created."); }
            else { return Ok(mapper.Map<AssignmentDTO>(assignment)); }

        }
        [HttpGet("GetAssignment")]
        public async Task<IActionResult> GetAssignment()
        {
            var assignments = await assignmentRepository.GetAssignment();
            if (assignments == null) { return BadRequest("No assignments found."); }
            return Ok(assignments);
        }
        [HttpGet("GetAssignmentById/{AssignmentId}")]
        public async Task<IActionResult> GetAssignmentById([FromRoute] string AssignmentId)
        {
            var assignment = await assignmentRepository.GetAssignmentById(AssignmentId);
            if (assignment == null) { return BadRequest("No assignment found."); }
            return Ok(assignment);
        }
        [HttpPut("UpdateAssignment/{AssignmentId}")]
        public async Task<IActionResult> UpdateAssignment(
            [FromRoute] string AssignmentId,
            [FromBody] UpdateAssignmentDTO updateAssignmentDTO,
            [FromForm] FileUploadRequestDTO imageUploadRequestDTO)
        {
            var assignment = await assignmentRepository.UpdateAssignment(AssignmentId, 
                mapper.Map<Assignment>(updateAssignmentDTO),
                imageUploadRequestDTO);
            if (assignment == null) { return BadRequest("Assignment couldn't be updated."); }
            return Ok(assignment);
        }
        [HttpDelete("DeleteAssignment/{AssignmentId}")]
        public async Task<IActionResult> DeleteAssignment([FromRoute]string AssignmentId)
        {
            var assignment = await assignmentRepository.DeleteAssignment(AssignmentId);
            if (assignment == null) { return BadRequest("Assignment couldn't be deleted."); }
            return Ok(assignment);
        }
        //[HttpPost("SubmitAssignment")]
        //public async Task<IActionResult> SubmitAssignment([FromBody])
        //{

        //}
    }
}

