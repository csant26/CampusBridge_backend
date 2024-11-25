using AutoMapper;
using backend.CustomActionFilter;
using backend.Models.Domain.Content.Help;
using backend.Models.DTO.Content.Help;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IHelpRepository helpRepository;
        private readonly IMapper mapper;

        public HelpController(IHelpRepository helpRepository, IMapper mapper)
        {
            this.helpRepository = helpRepository;
            this.mapper = mapper;
        }
        [HttpPost("CreateQuestion")]
        [ValidateModel]
        public async Task<IActionResult> CreateQuestion([FromBody] AddQuestionDTO addQuestionDTO)
        {
            var question = await helpRepository.CreateQuestion(mapper.Map<Question>(addQuestionDTO));
            if (question == null) { return BadRequest("Question couldn't be created."); }
            return Ok(mapper.Map<QuestionDTO>(question));
        }
        [HttpGet("GetQuestion")]
        [ValidateModel]
        public async Task<IActionResult> GetQuestion()
        {
            var questions = await helpRepository.GetQuestion();
            if (questions == null) { return BadRequest("No questions found."); }
            return Ok(mapper.Map<List<QuestionDTO>>(questions));
        }
        [HttpGet("GetQuestionById/{QuestionId}")]
        [ValidateModel]
        public async Task<IActionResult> GetQuestionById([FromRoute] string QuestionId)
        {
            var question = await helpRepository.GetQuestionById(QuestionId);
            if (question == null) { return BadRequest("No question found."); }
            return Ok(mapper.Map<QuestionDTO>(question));
        }
        [HttpGet("GetQuestionById/{RoleName}")]
        [ValidateModel]
        public async Task<IActionResult> GetQuestionByRole([FromRoute] string RoleName)
        {
            var questions = await helpRepository.GetQuestionByRole(RoleName);
            if (questions == null) { return BadRequest("No question found."); }
            return Ok(mapper.Map<List<QuestionDTO>>(questions));
        }
        [HttpPut("UpdateQueston/{QuestionId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateQuestion(string QuestionId,
            [FromBody] UpdateQuestionDTO updateQuestionDTO)
        {
            var question = await helpRepository.UpdateQuestion(QuestionId,
                mapper.Map<Question>(updateQuestionDTO));
            if (question == null) { return BadRequest("Question couldn't be updated."); }
            return Ok(mapper.Map<QuestionDTO>(question));
        }
        [HttpDelete("DeleteQuestion/{QuestionId}/{CreatorId}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] string QuestionId,
            [FromRoute] string CreatorId)
        {
            var question = await helpRepository.DeleteQuestion(QuestionId, CreatorId);
            if (question == null) { return BadRequest("Question couldn't be deleted."); }
            return Ok(mapper.Map<QuestionDTO>(question));
        }
        [HttpPost("AddAnswer")]
        [ValidateModel]
        public async Task<IActionResult> AddAnswer([FromBody] AddAnswerDTO addAnswerDTO)
        {
            var answer = await helpRepository.AddAnswer(mapper.Map<Answer>(addAnswerDTO));
            if (answer == null) { return BadRequest("Answer couldn't be added."); }
            return Ok(mapper.Map<AnswerDTO>(answer));
        }
        [HttpGet("GetAnswer")]
        [ValidateModel]
        public async Task<IActionResult> GetAnswer()
        {
            var answers = await helpRepository.GetAnswer();
            if (answers == null) { return BadRequest("No answers found."); }
            return Ok(mapper.Map<List<AnswerDTO>>(answers));
        }
        [HttpGet("GetAnswerById/{AnswerId}")]
        [ValidateModel]
        public async Task<IActionResult> GetAnswerById([FromRoute] string AnswerId)
        {
            var answer = await helpRepository.GetAnswerById(AnswerId);
            if (answer == null) { return BadRequest("No answers found."); }
            return Ok(mapper.Map<AnswerDTO>(answer));
        }
        [HttpGet("GetAnswerByRoleId/{RoleId}")]
        [ValidateModel]
        public async Task<IActionResult> GetAnswerByRoleId([FromRoute] string RoleId)
        {
            var answer = await helpRepository.GetAnswerByRoleId(RoleId);
            if (answer == null) { return BadRequest("No answers found."); }
            return Ok(mapper.Map<List<AnswerDTO>>(answer));
        }
        [HttpPut("UpdateAnswer/{AnswerId}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAnswer([FromRoute] string AnswerId,
            [FromBody] UpdateAnswerDTO updateAnswerDTO)
        {
            var answer = helpRepository.UpdateAnswer(AnswerId,
                mapper.Map<Answer>(updateAnswerDTO));
            if (answer == null) { return BadRequest("Answer couldn't be updated."); }
            return Ok(mapper.Map<Answer>(answer));
        }
        [HttpDelete("DeleteAnswer/{AnswerId}/{CreatorId}")]
        [ValidateModel]
        public async Task<IActionResult> DeleteAnswer([FromRoute] string AnswerId,
            [FromRoute] string CreatorId)
        {
            var answer = helpRepository.DeleteAnswer(AnswerId, CreatorId);
            if (answer == null) { return BadRequest("Answer couldn't be deleted."); }
            return Ok(mapper.Map<AnswerDTO>(answer));
        }
    }
}