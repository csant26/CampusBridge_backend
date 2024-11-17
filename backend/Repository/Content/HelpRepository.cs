using backend.Data;
using backend.Models.Domain.Content.Help;
using backend.Models.Domain.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class HelpRepository : IHelpRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public HelpRepository(CampusBridgeDbContext campusBridgeDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
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
            Question question)
        {
            var existingQuestion = await GetQuestionById(QuestionId);
            if (existingQuestion == null) { return null; }

            if (existingQuestion.StudentId != question.StudentId) { return null; }

            existingQuestion.QuestionDetails = question.QuestionDetails;
            existingQuestion.DirectedTo = question.DirectedTo;
            await campusBridgeDbContext.SaveChangesAsync();
            return existingQuestion;
        }

        public async Task<Question> DeleteQuestion(string QuestionId, string StudentId)
        {
            var existingQuestion = await GetQuestionById(QuestionId);
            if (existingQuestion == null) { return null; }
            if (existingQuestion.StudentId != StudentId) { return null; }
            campusBridgeDbContext.Questions.Remove(existingQuestion);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingQuestion;
        }

        public async Task<Answer> AddAnswer(Answer answer)
        {
            var user = await userManager.FindByEmailAsync(answer.AnswerById);
            var roles = await userManager.GetRolesAsync(user);
            var question = await campusBridgeDbContext.Questions.FindAsync(answer.QuestionId);
            foreach(var role in roles)
            {
                if (!question.DirectedTo.Contains(role))
                {
                    return null;
                }
            }
            answer.Question = question;
            await campusBridgeDbContext.Answers.AddAsync(answer);
            await campusBridgeDbContext.SaveChangesAsync();
            return answer;
        }

        public async Task<List<Answer>> GetAnswer()
        {
            var answers = await campusBridgeDbContext.Answers
                .Include(x => x.Question)
                .ToListAsync();
            if (answers == null) { return null;  }
            return answers;
        }
        public async Task<Answer> GetAnswerById(string AnswerId)
        {
            var answer = await campusBridgeDbContext.Answers
                .Include(x => x.Question)
                .FirstOrDefaultAsync(x=>x.AnswerId == AnswerId);
            if (answer == null) { return null; }
            return answer;
        }
        public async Task<List<Answer>> GetAnswerByRoleId(string RoleId)
        {
            var answers = await campusBridgeDbContext.Answers.Where(x => x.AnswerById == RoleId).ToListAsync();
            if(answers == null) { return null; }
            return answers;
            
        }
        public async Task<Answer> UpdateAnswer(string AnswerId, Answer answer)
        {
            var exisitingAnswer = await GetAnswerById(AnswerId);
            if (exisitingAnswer == null) { return null; }

            if (exisitingAnswer.AnswerById != answer.AnswerById) { return null; }

            exisitingAnswer.AnswerDetails = answer.AnswerDetails;
            await campusBridgeDbContext.SaveChangesAsync();
            return exisitingAnswer;
        }
        public async Task<Answer> DeleteAnswer(string AnswerId, string CreatorId)
        {
            var existingAnswer = await GetAnswerById(AnswerId);
            if (existingAnswer == null) { return null; }
            if (existingAnswer.AnswerById != CreatorId) { return null; }
            campusBridgeDbContext.Answers.Remove(existingAnswer);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingAnswer;
        }
    }
}
