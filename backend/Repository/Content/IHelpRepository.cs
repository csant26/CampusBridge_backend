using backend.Models.Domain.Content.Help;

namespace backend.Repository.Content
{
    public interface IHelpRepository
    {
        Task<Question> CreateQuestion(Question question);
        Task<List<Question>> GetQuestion();
        Task<Question> GetQuestionById(string QuestionId);
        Task<List<Question>> GetQuestionByRole(string RoleName);
        Task<Question> UpdateQuestion (string QuestionId, string StudentId, Question question);
        Task<Question> DeleteQuestion(string QuestionId);

        Task<Answer> AddAnswer(string CreatorId, Answer answer);
        Task<List<Answer>> GetAnswer();
        Task<Answer> GetAnswerById(string AnswerId);
        Task<Answer> UpdateAnswer(string AnswerId, string CreatorId, Answer answer);
        Task<Answer> DeleteAnswer(string AnswerId, string CreatorId);
    }
}
