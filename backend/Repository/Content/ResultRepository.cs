using backend.Data;
using backend.Models.Domain.Content.Results;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class ResultRepository : IResultRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public ResultRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Result> CreateResult(Result result)
        {
            var student = await campusBridgeDbContext.Students.FindAsync(result.StudentId);
            if (student == null) { return null; }
            result.Student = student;
            await campusBridgeDbContext.Results.AddAsync(result);
            await campusBridgeDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<Result>> GetResult()
        {
            return await campusBridgeDbContext.Results.Include(s => s.Student).ToListAsync();
        }

        public async Task<Result> GetResultById(string ResultId)
        {
            var result = await campusBridgeDbContext.Results.FindAsync(ResultId);
            if (result == null) { return null; }
            return result;
        }

        public async Task<Result> UpdateResult(string ResultId, Result result)
        {
            var existingResult = await GetResultById(ResultId);
            if (existingResult == null) { return null; }
            var student = await campusBridgeDbContext.Students.FindAsync(result.StudentId);
            if (student == null) { return null; }
            existingResult.ExaminationType = result.ExaminationType;
            existingResult.Semester= result.Semester;
            existingResult.Percentage = result.Percentage;
            existingResult.Status = result.Status;
            existingResult.Student = student;
            existingResult.StudentId = result.StudentId;
            await campusBridgeDbContext.SaveChangesAsync();
            return existingResult;
        }
        public async Task<Result> DeleteResult(string ResultId)
        {
            var existingResult = await GetResultById(ResultId);
            if (existingResult == null) { return null; }
            campusBridgeDbContext.Results.Remove(existingResult);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingResult;
        }
    }
}
