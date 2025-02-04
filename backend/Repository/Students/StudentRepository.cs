using backend.Data;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Students;
using backend.Models.DTO.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public StudentRepository(CampusBridgeDbContext campusBridgeDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<Student> CreateStudent(Student student, AddStudentDTO addStudentDTO)
        {
            var academic = await campusBridgeDbContext.Academics.FindAsync(addStudentDTO.AcademicId);
            var financial = await campusBridgeDbContext.Financials.FindAsync(addStudentDTO.FinancialId);
            var clubs = await campusBridgeDbContext.Clubs
                .Where(c => addStudentDTO.ClubIds.Contains(c.ClubId)).ToListAsync();

            if (academic != null)
            {
                student.Academic = academic;
                var sem = academic.Semester;
                var syllabus = await campusBridgeDbContext.Syllabus
                    .Where(x => x.Semester == academic.Semester)
                    .Include(x => x.Courses)
                    .FirstOrDefaultAsync();
                if (syllabus != null)
                {
                    var nonElectiveCourses = syllabus.Courses.Where(x => x.isElective == false).ToList();
                    if (nonElectiveCourses != null)
                    {
                        student.Courses = nonElectiveCourses;
                    }
                    var allowedElectives = Convert.ToInt32(syllabus.AllowedElectiveNo);
                    var electiveCourses = syllabus.Courses.Where(x => x.isElective == true).ToList();
                    var selectedElectives = await campusBridgeDbContext.Course
                        .Where(x => addStudentDTO.ElectiveIds.Contains(x.CourseId))
                        .ToListAsync();
                    var electivesToAdd = selectedElectives
                        .Where(elective => electiveCourses.Contains(elective))
                        .Take(allowedElectives)
                        .ToList();
                    //foreach (var elective in selectedElectives)
                    //{
                    //    if (electiveCourses.Contains(elective))
                    //    {
                    //        while (allowedElectives > 0)
                    //        {
                    //            electivesToAdd.AddRange(new List<Course> { elective }); 
                    //            allowedElectives--;
                    //        }
                    //    }
                    //}
                    student.Courses.AddRange(electivesToAdd);
                }
            }
            if (financial != null)
            {
                student.Financial = financial;
            }
            if (clubs != null)
            {
                student.Clubs = clubs;
            }

            var college = await campusBridgeDbContext.Colleges.Where(x => x.Email == addStudentDTO.CollegeId).FirstOrDefaultAsync();
            if (college != null) { student.College = college; }



            //Register in the authenticated users.
            if (userManager.Users.All(u => u.Email != student.Email))
            {
                var studentUser = new IdentityUser
                {
                    UserName = student.Email,
                    Email = student.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(studentUser, student.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(studentUser, "Student");
                }

                if (student.isClubHead == true)
                {
                    await userManager.AddToRoleAsync(studentUser, "ClubHead");
                }
                if (student.isAuthor == true)
                {
                    await userManager.AddToRoleAsync(studentUser, "Author");
                }

                await campusBridgeDbContext.Students.AddAsync(student);
                await campusBridgeDbContext.SaveChangesAsync();

                return student;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Student>> GetStudent()
        {
            var students = await campusBridgeDbContext.Students
                .Include(a => a.Academic)
                .Include(f => f.Financial)
                .Include(c => c.Clubs)
                .Include(x => x.College)
                .Include(x => x.Courses)
                .ToListAsync();
            if (students != null) { return students; }
            else { return null; }
        }
        public async Task<Student> GetStudentById(string id)
        {
            var existingStudent = await campusBridgeDbContext.Students
                .Include(a => a.Academic).Include(f => f.Financial).Include(c => c.Clubs).Include(x => x.College)
                .FirstOrDefaultAsync(x => (x.StudentId == id) || (x.Email == id));
            if (existingStudent != null) { return existingStudent; }
            else { return null; }
        }
        public async Task<Student> UpdateStudent(string id, Student updatedStudent,
            UpdateStudentDTO updateStudentDTO)
        {
            var existingStudent = await GetStudentById(id);

            if (existingStudent!=null)
            {
                var existingCollegeUser = await userManager.FindByEmailAsync(updatedStudent.CollegeId);
                if (existingCollegeUser == null) { return null; }
                var roles = await userManager.GetRolesAsync(existingCollegeUser);
                if (!roles.Contains("College") && existingStudent.StudentId != updatedStudent.CollegeId)
                {
                    return null;
                }

                var existingUser = await userManager.FindByEmailAsync(existingStudent.Email);
                if (existingUser == null) { return null; }
                await userManager.DeleteAsync(existingUser);

                existingStudent.Name = updatedStudent.Name;
                existingStudent.Phone = updatedStudent.Phone;
                existingStudent.Location = updatedStudent.Location;
                existingStudent.isAuthor = updatedStudent.isAuthor;
                existingStudent.isClubHead = updatedStudent.isClubHead;
                
                if(existingStudent.StudentId == updatedStudent.CollegeId)
                {
                    existingStudent.Email = updatedStudent.Email;
                    existingStudent.Password = updatedStudent.Password;
                }

                var syllabus = await campusBridgeDbContext.Syllabus
                    .Where(x => x.Semester == updateStudentDTO.Semseter)
                    .FirstOrDefaultAsync();
                if (syllabus != null)
                {
                    var nonElectiveCourses = syllabus.Courses.Where(x => x.isElective == false).ToList();
                    if (nonElectiveCourses != null)
                    {
                        updatedStudent.Courses = nonElectiveCourses;
                    }
                    var allowedElectives = Convert.ToInt32(syllabus.AllowedElectiveNo);
                    var electiveCourses = syllabus.Courses.Where(x => x.isElective == true).ToList();
                    var selectedElectives = await campusBridgeDbContext.Course
                        .Where(x => updateStudentDTO.ElectiveIds.Contains(x.CourseId))
                        .ToListAsync();
                    var electivesToAdd = new List<Course>();
                    foreach (var elective in selectedElectives)
                    {
                        if (electiveCourses.Contains(elective))
                        {
                            while (allowedElectives > 0)
                            {
                                electivesToAdd.AddRange(new List<Course> { elective });
                                allowedElectives--;
                            }
                        }
                    }
                    updatedStudent.Courses.AddRange(electivesToAdd);

                }

                var financial = await campusBridgeDbContext.Financials.FindAsync(updateStudentDTO.FinancialId);
                var clubs = await campusBridgeDbContext.Clubs
                    .Where(m => updateStudentDTO.ClubIds.Contains(m.ClubId)).ToListAsync();

                if (financial != null)
                {
                    existingStudent.Financial = financial;
                }
                if (clubs != null)
                {
                    existingStudent.Clubs = clubs;
                }

                await campusBridgeDbContext.SaveChangesAsync();

                if(userManager.Users.All(u=>u.Email== existingStudent.Email)) { return null; }

                var studentUser = new IdentityUser
                {
                    UserName = existingStudent.Email,
                    Email = existingStudent.Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(studentUser, existingStudent.Password);
                await userManager.AddToRoleAsync(studentUser, "Student");

                if (existingStudent.isClubHead == true)
                {
                    await userManager.AddToRoleAsync(studentUser, "ClubHead");
                }
                if (existingStudent.isAuthor == true)
                {
                    await userManager.AddToRoleAsync(studentUser, "Author");
                }

            }
            else
            {
                return null;
            }
            return existingStudent;
        }
        public async Task<Student> DeleteStudent(string StudentId, string CollegeId)
        {
            var existingStudent = await GetStudentById(StudentId);
            if (existingStudent == null) { return null; }
            
            var existingCollegeUser = await userManager.FindByEmailAsync(CollegeId);
            if (existingCollegeUser == null) { return null; }
            var roles = await userManager.GetRolesAsync(existingCollegeUser);
            if (!roles.Contains("College")) { return null; }

            campusBridgeDbContext.Students.Remove(existingStudent);
            await campusBridgeDbContext.SaveChangesAsync();

            var existingUser = await userManager.FindByEmailAsync(existingStudent.Email);
            if (existingUser == null) { return null; }
            await userManager.DeleteAsync(existingUser);

            return existingStudent;
        }
    }
}
