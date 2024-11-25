using backend.Models.Domain.Content.Help;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface IHelpRepository
    {
        Task<Question> CreateQuestion(Question question);
        Task<List<Question>> GetQuestion();
        Task<Question> GetQuestionById(string QuestionId);
        Task<List<Question>> GetQuestionByRole(string RoleName);
        Task<Question> UpdateQuestion (string QuestionId,Question question);
        Task<Question> DeleteQuestion(string QuestionId,string StudentId);

        Task<Answer> AddAnswer(Answer answer);
        Task<List<Answer>> GetAnswer();
        Task<Answer> GetAnswerById(string AnswerId);
        Task<List<Answer>> GetAnswerByRoleId(string RoleId);
        Task<Answer> UpdateAnswer(string AnswerId, Answer answer);
        Task<Answer> DeleteAnswer(string AnswerId, string CreatorId);
    }
}
