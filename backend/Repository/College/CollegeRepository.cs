using backend.Data;
using backend.Models.Domain.Student;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.College
{
    public class CollegeRepository : ICollegeRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public CollegeRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Student> CreateStudent(Student student)
        {
            await campusBridgeDbContext.Students.AddAsync(student);
            await campusBridgeDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetStudent()
        {
            return await campusBridgeDbContext.Students
                .Include("Academic")
                .Include("Financial")
                .Include("Contact")
                .ToListAsync();
        }
    }
}
