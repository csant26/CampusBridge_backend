using backend.Data;
using backend.Models.Domain.Content.Help;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class HelpRepository : IHelpRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public HelpRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Question> CreateQuestion(Question question)
        {
            var student = await campusBridgeDbContext.Students.FindAsync(question.StudentId);
            if (student != null)
            {
                question.Student = student;
            }
            await campusBridgeDbContext.AddAsync(question);
            await campusBridgeDbContext.SaveChangesAsync();
            return question;
        }
        public async Task<List<Question>> GetQuestion()
        {
            var questions = await campusBridgeDbContext.Questions
                .Include(st=>st.Student).Include(ans=>ans.Answers)
                .ToListAsync();
            if (questions == null) { return null; }
            return questions;
        }

        public async Task<Question> GetQuestionById(string QuestionId)
        {
            var question = await campusBridgeDbContext.Questions
                .Include(st => st.Student).Include(ans => ans.Answers)
                .FirstOrDefaultAsync(x => x.QuestionId == QuestionId);
            if (question == null) { return null; }
            return question;
        }

        public async Task<List<Question>> GetQuestionByRole(string RoleName)
        {
            var questionsToReturn = new List<Question>();
            var questions = await GetQuestion();
            foreach(var question in questions)
            {
                if (question.DirectedTo.Contains(RoleName))
                {
                    questionsToReturn.AddRange(question);
                }
            }
            return questionsToReturn;
        }
        public async Task<Question> UpdateQuestion(string QuestionId,
            string StudentId,
            Question question)
        {
            var existingQuestion = await GetQuestionById(QuestionId);
            if (existingQuestion == null) { return null; }

            if (existingQuestion.StudentId == StudentId)
            {
                existingQuestion.QuestionDetails = question.QuestionDetails;
                existingQuestion.DirectedTo = question.DirectedTo;
            }
            await campusBridgeDbContext.SaveChangesAsync();
            return existingQuestion;
        }

        public Task<Question> DeleteQuestion(string QuestionId)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> AddAnswer(string CreatorId, Answer answer)
        {
            throw new NotImplementedException();
        }

        public Task<List<Answer>> GetAnswer()
        {
            throw new NotImplementedException();
        }
        public Task<Answer> GetAnswerById(string AnswerId)
        {
            throw new NotImplementedException();
        }
        public Task<Answer> UpdateAnswer(string AnswerId, string CreatorId, Answer answer)
        {
            throw new NotImplementedException();
        }
        public Task<Answer> DeleteAnswer(string AnswerId, string CreatorId)
        {
            throw new NotImplementedException();
        }
    }
}
