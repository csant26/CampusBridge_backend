using backend.Data;
using backend.Models.Domain.Colleges;
using backend.Models.Domain.Teachers;
using backend.Models.DTO.Content.Schedule;
using backend.Models.DTO.Teacher;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository.Teachers
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public TeacherRepository(CampusBridgeDbContext campusBridgeDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }

        public async Task<Teacher> CreateTeacher(Teacher teacher, AddTeacherDTO addTeacherDTO)
        {
            var existingCollege = await campusBridgeDbContext.Colleges.Where(x => x.Email == addTeacherDTO.CollegeId).FirstOrDefaultAsync();
            if (existingCollege == null) { return null; }
            teacher.Colleges.AddRange(new List<College> { existingCollege });

            var courses = await campusBridgeDbContext.Course
                .Where(x => addTeacherDTO.CourseIds.Contains(x.CourseId))
                .ToListAsync();
            teacher.Courses = courses;

            if (userManager.Users.All(u => u.Email != teacher.Email))
            {
                var newTeacherUser = new IdentityUser
                {
                    Email = teacher.Email,
                    UserName = teacher.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newTeacherUser, teacher.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newTeacherUser, "Teacher");
                    await userManager.AddToRoleAsync(newTeacherUser, "Author");

                    await campusBridgeDbContext.Teachers.AddAsync(teacher);
                    await campusBridgeDbContext.SaveChangesAsync();


                    return teacher;
                }
            }
            return null;

        }
        public async Task<List<Teacher>> GetTeacher()
        {
            return await campusBridgeDbContext.Teachers
                .Include(c => c.Colleges).Include(co => co.Courses)
                .ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(string TeacherId)
        {
            var teacher = await campusBridgeDbContext.Teachers
                .Include(c => c.Colleges).Include(co => co.Courses)
                .FirstOrDefaultAsync(x => x.TeacherId == TeacherId);
            if (teacher == null) { return null; }
            return teacher;
        }
        public async Task<List<Teacher>> GetTeacherBySemester(string Semester)
        {
            List<Teacher> teachers = new List<Teacher>();
            var syllabus = await campusBridgeDbContext.Syllabus.FirstOrDefaultAsync(x => x.Semester == Semester);
            var courses = await campusBridgeDbContext.Course.Where(x => x.SyllabusId == syllabus.SyllabusId).ToListAsync();
            foreach(var course in courses)
            {
                Teacher teacher = new Teacher();
                teacher = await campusBridgeDbContext.Teachers
                .Include(c => c.Colleges).Include(co => co.Courses)
                .FirstOrDefaultAsync(x => x.Courses.Contains(course));
                if (teacher != null) { teachers.Add(teacher); }
            }
            return teachers;
        }
        public async Task<Teacher> UpdateTeacher(string TeacherId, Teacher teacher, UpdateTeacherDTO updateTeacherDTO)
        {
            var existingTeacher = await GetTeacherById(TeacherId);
            if (existingTeacher == null) { return null; }
            var existingCollegeUser = await userManager.FindByEmailAsync(updateTeacherDTO.CreatorId);
            if (existingCollegeUser == null) { return null; }
            var roles = await userManager.GetRolesAsync(existingCollegeUser);
            if (!roles.Contains("College") && 
                existingTeacher.TeacherId!=updateTeacherDTO.CreatorId) { return null; }

            var existingTeacherUser = await userManager.FindByEmailAsync(teacher.Email);
            if (existingTeacherUser == null) { return null; }
            await userManager.DeleteAsync(existingTeacherUser);

            existingTeacher.Name = updateTeacherDTO.Name;
            existingTeacher.Phone = updateTeacherDTO.Phone;

            var courses = await campusBridgeDbContext.Course
                .Where(x => updateTeacherDTO.CourseIds.Contains(x.CourseId)).ToListAsync();
            if (courses.Any()){ existingTeacher.Courses = courses; }

            if(existingTeacher.TeacherId == updateTeacherDTO.CreatorId)
            {
                existingTeacher.Email = updateTeacherDTO.Email;
                existingTeacher.Password = updateTeacherDTO.Password;
            }
            await campusBridgeDbContext.SaveChangesAsync();

            var newTeacherUser = new IdentityUser
            {
                Email = existingTeacher.Email,
                UserName = existingTeacher.Email,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(newTeacherUser, existingTeacher.Password);
            await userManager.AddToRoleAsync(newTeacherUser, "Teacher");
            return existingTeacher;
        }
        public async Task<Teacher> DeleteTeacher(string TeacherId, string CollegeId)
        {
            var existingTeacher = await GetTeacherById(TeacherId);
            if (existingTeacher == null) { return null; }

            var existingCollege = await campusBridgeDbContext.Colleges.FirstOrDefaultAsync(x=>x.Email == CollegeId);
            if (existingCollege == null) { return null; }

            var existingCollegeUser = await userManager.FindByEmailAsync(existingCollege.Email);
            if (existingCollegeUser == null) { return null; }

            var roles = await userManager.GetRolesAsync(existingCollegeUser);
            if (!roles.Contains("College")) { return null; }

            var existingTeacherUser = await userManager.FindByEmailAsync(existingTeacher.Email);
            await userManager.DeleteAsync(existingTeacherUser);

            campusBridgeDbContext.Teachers.Remove(existingTeacher);
            await campusBridgeDbContext.SaveChangesAsync();

            return existingTeacher;
        }
        public async Task<List<CourseTeacherResult>> GetCourseTeacherDataAsync()
        {
            string query = @"
            SELECT C.CourseTitle, T.TeacherId 
            FROM dbo.CourseTeacher(NOLOCK) CT
            LEFT JOIN dbo.Course(NOLOCK) C ON CT.CoursesCourseId = C.CourseId
            LEFT JOIN dbo.Teachers(NOLOCK) T ON CT.TeachersTeacherId = T.TeacherId";

            // Execute the raw SQL and map it to CourseTeacherResult
            return await campusBridgeDbContext.Set<CourseTeacherResult>().FromSqlRaw(query).ToListAsync();
        }
    }
}
