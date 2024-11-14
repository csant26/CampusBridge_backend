using backend.Data;
using backend.Models.Domain.Student;
using backend.Models.DTO.Student;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.College
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public StudentRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Student> CreateStudent(Student student, AddStudentDTO addStudentDTO)
        {
            var academic = await campusBridgeDbContext.Academics.FindAsync(addStudentDTO.AcademicId);
            var financial = await campusBridgeDbContext.Financials.FindAsync(addStudentDTO.FinancialId);
            var majors = await campusBridgeDbContext.Majors
                .Where(m => addStudentDTO.MajorIds.Contains(m.MajorId)).ToListAsync();
            var clubs = await campusBridgeDbContext.Clubs
                .Where(c => addStudentDTO.ClubIds.Contains(c.ClubId)).ToListAsync();

            if(academic != null)
            {
                student.Academic = academic;
            }
            if(financial != null)
            {
                student.Financial = financial;
            }
            if(majors != null)
            {
                student.Majors = majors;
            }
            if(clubs != null)
            {
                student.Clubs = clubs;
            }

            await campusBridgeDbContext.Students.AddAsync(student);
            await campusBridgeDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetStudent()
        {
            var students = await campusBridgeDbContext.Students
                .Include(a=>a.Academic)
                .Include(f=>f.Financial)
                .Include(m=>m.Majors)
                .Include(c=>c.Clubs)
                .ToListAsync();
            if(students != null) { return students; }
            else { return null; }
        }
        public async Task<Student> GetStudentById(string id)
        {
            var existingStudent = await campusBridgeDbContext.Students
                .Include(a => a.Academic).Include(f => f.Financial).Include(c => c.Clubs).Include(m => m.Majors)
                .FirstOrDefaultAsync(x => x.StudentId == id);
            if (existingStudent != null) { return existingStudent; }
            else { return null; }
        }
        public async Task<Student> UpdateStudent(string id, Student updatedStudent,
            UpdateStudentDTO updateStudentDTO)
        {
            var existingStudent = await GetStudentById(id);

            if (existingStudent!=null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.Email = updatedStudent.Email;
                existingStudent.Phone = updatedStudent.Phone;
                existingStudent.Location = updatedStudent.Location;

                var financial = await campusBridgeDbContext.Financials.FindAsync(updateStudentDTO.FinancialId);
                var majors = await campusBridgeDbContext.Majors
                    .Where(m => updateStudentDTO.MajorIds.Contains(m.MajorId)).ToListAsync();
                var clubs = await campusBridgeDbContext.Clubs
                    .Where(m => updateStudentDTO.ClubIds.Contains(m.ClubId)).ToListAsync();

                if (financial != null)
                {
                    existingStudent.Financial = financial;
                }
                if (majors != null)
                {
                    existingStudent.Majors = majors;
                }
                if (clubs != null)
                {
                    existingStudent.Clubs = clubs;
                }

                await campusBridgeDbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            return existingStudent;
        }
        public async Task<Student> DeleteStudent(string id)
        {
            var existingStudent = await GetStudentById(id);
            if (existingStudent == null) { return null; }
            campusBridgeDbContext.Students.Remove(existingStudent);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingStudent;
        }
    }
}
