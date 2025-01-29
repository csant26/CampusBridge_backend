using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Assignments;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.File;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IMapper mapper;
        public AssignmentController(IAssignmentRepository assignmentRepository, IMapper mapper)
        {
            this.assignmentRepository = assignmentRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateAssignment")]
        [ValidateModel]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAssignment(
            [FromForm] AddAssignmentDTO addAssignmentDTO,
            [FromForm] FileUploadRequestDTO fileUploadRequestDTO)
        {
            var assignment = await assignmentRepository.CreateAssignment(
                mapper.Map<Assignment>(addAssignmentDTO),
                fileUploadRequestDTO);
            if (assignment == null) { return BadRequest("Assignment couldn't be created."); }
            else { return Ok(mapper.Map<AssignmentDTO>(assignment)); }
        }
        [HttpGet("GetAssignment")]
        [ValidateModel]
        public async Task<IActionResult> GetAssignment()
        {
            var assignments = await assignmentRepository.GetAssignment();
            if (assignments == null) { return BadRequest("No assignments found."); }
            return Ok(mapper.Map<List<AssignmentDTO>>(assignments));
        }
        [HttpGet("GetAssignmentById/{AssignmentId}")]
        [ValidateModel]
        public async Task<IActionResult> GetAssignmentById([FromRoute] string AssignmentId)
        {
            var assignment = await assignmentRepository.GetAssignmentById(AssignmentId);
            if (assignment == null) { return BadRequest("No assignment found."); }
            return Ok(mapper.Map<AssignmentDTO>(assignment));
        }
        [HttpGet("GetAssignmentByTeacherId/{TeacherId}")]
        [ValidateModel]
        public async Task<IActionResult> GetAssignmentByTeacherId([FromRoute] string TeacherId)
        {
            var assignment = await assignmentRepository.GetAssignmentByTeacherId(TeacherId);
            if (assignment == null) { return BadRequest("No assignment found."); }
            return Ok(mapper.Map<List<AssignmentDTO>>(assignment));
        }
        [HttpPut("UpdateAssignment/{AssignmentId}/{TeacherId}")]
        [ValidateModel]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateAssignment(
            [FromRoute] string AssignmentId,
            [FromBody] UpdateAssignmentDTO updateAssignmentDTO,
            [FromForm] FileUploadRequestDTO imageUploadRequestDTO)
        {
            var assignment = await assignmentRepository.UpdateAssignment(AssignmentId,
                mapper.Map<Assignment>(updateAssignmentDTO),
                imageUploadRequestDTO);
            if (assignment == null) { return BadRequest("Assignment couldn't be updated."); }
            return Ok(mapper.Map<AssignmentDTO>(assignment));
        }
        [HttpDelete("DeleteAssignment/{AssignmentId}/{TeacherId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteAssignment(
            [FromRoute]string TeacherId,
            [FromRoute] string AssignmentId)
        {
            var assignment = await assignmentRepository.DeleteAssignment(AssignmentId,TeacherId);
            if (assignment == null) { return BadRequest("Assignment couldn't be deleted."); }
            return Ok(mapper.Map<AssignmentDTO>(assignment));
        }
        [HttpPost("SubmitAssignment")]
        [ValidateModel]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubmitAssignment([FromForm] AddSubmissionDTO addSubmissionDTO,
            [FromForm] FileUploadRequestDTO fileUploadRequestDTO)
        {
            var submittedAssignment = await assignmentRepository
                .SubmitAssignment(mapper.Map<Submission>(addSubmissionDTO), fileUploadRequestDTO);
            if (submittedAssignment == null) { return BadRequest("Assignment couldn't be submitted."); }
            return Ok(mapper.Map<SubmissionDTO>(submittedAssignment));
        }
        [HttpGet("GetSubmission")]
        [ValidateModel]
        public async Task<IActionResult> GetSubmission()
        {
            var submissions = await assignmentRepository.GetSubmission();
            if (submissions == null) { return BadRequest("No submissions found."); }
            return Ok(mapper.Map<List<SubmissionDTO>>(submissions));
        }
        [HttpGet("GetSubmissionById/{SubmissionId}")]
        [ValidateModel]
        public async Task<IActionResult> GetSubmissionById([FromRoute] string SubmissionId)
        {
            var submission = await assignmentRepository.GetSubmissionById(SubmissionId);
            if (submission == null) { return BadRequest("No submissions found."); }
            return Ok(mapper.Map<SubmissionDTO>(submission));
        }
        [HttpGet("GetSubmissionByAssignmentId/{AssignmentId}")]
        [ValidateModel]
        public async Task<IActionResult> GetSubmissionByAssignmentId([FromRoute] string AssignmentId)
        {
            var submission = await assignmentRepository.GetSubmissionByAssignmentId(AssignmentId);
            if (submission == null) { return BadRequest("No submissions found."); }
            return Ok(mapper.Map<List<SubmissionDTO>>(submission));
        }
        [HttpGet("GetSubmissionByStudentId/{AssignmentId}/{StudentId}")]
        [ValidateModel]
        public async Task<IActionResult> GetSubmissionByStudentId([FromRoute] string AssignmentId, [FromRoute] string StudentId)
        {
            var submission = await assignmentRepository.GetSubmissionByStudentId(AssignmentId,StudentId);
            if (submission == null) { return BadRequest("No submissions found."); }
            return Ok(mapper.Map<SubmissionDTO>(submission));
        }
        [HttpGet("GetStudentSubmissions/{StudentId}")]
        [ValidateModel]
        public async Task<IActionResult> GetStudentSubmissions([FromRoute] string StudentId)
        {
            var submission = await assignmentRepository.GetStudentSubmissions(StudentId);
            if (submission == null) { return BadRequest("No submissions found."); }
            return Ok((submission));
        }
        [HttpPut("UpdateSubmission/{SubmissionId}")]
        [ValidateModel]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateSubmission([FromRoute] string SubmissionId,
            [FromForm] UpdateSubmissionDTO updateSubmissionDTO,
            [FromForm] FileUploadRequestDTO fileUploadRequestDTO)
        {
            var submission = await assignmentRepository.UpdateSubmission(SubmissionId,
                mapper.Map<Submission>(updateSubmissionDTO),
                fileUploadRequestDTO);
            if (submission == null) { return BadRequest("Submission unchanged."); }
            return Ok(mapper.Map<SubmissionDTO>(submission));
        }
        [HttpDelete("DeleteSubmission/{SubmissionId}/{StudentId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteSubmission([FromRoute] string SubmissionId,
            [FromRoute]string StudentId)
        {
            var submission = await assignmentRepository.DeleteSubmission(SubmissionId, StudentId);
            if (submission == null) { return BadRequest("Submission couldn't be deleted."); }
            return Ok(mapper.Map<SubmissionDTO>(submission));
        }
    }
}

