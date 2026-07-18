using backend.Data;
using backend.Models.Domain.Universities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace backend
{
    public class SeedData
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public SeedData(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            string[] roleNames = {
                "Developer",
                "University",
                "College",
                "Teacher",
                "Student",
                "ClubHead",
                "Author",
                "ClubMember"
            };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed the Developer (University Admin) user
            string developerEmail = "campus@bridge.com";
            string developerPassword = "KimCSar@123";

            if (userManager.Users.All(u => u.Email != developerEmail))
            {
                var developerUser = new IdentityUser
                {
                    UserName = developerEmail,
                    Email = developerEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(developerUser, developerPassword);
                await userManager.AddToRoleAsync(developerUser, "Developer");
            }

            string universityEmail = "university@gmail.com";
            string universityPassword = "University@123";
            if (userManager.Users.All(u => u.Email != universityEmail))
            {
                var universityUser = new IdentityUser
                {
                    UserName = universityEmail,
                    Email = universityEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(universityUser, universityPassword);
                await userManager.AddToRoleAsync(universityUser, "University");
            }

            var existingUniversityData = await campusBridgeDbContext.Universities.FindAsync(universityEmail);
            if (existingUniversityData == null)
            {
                var universityData = new University
                {
                    UniversityId = universityEmail,
                    Name = universityEmail,
                    Email = universityEmail,
                    Description = universityEmail,
                    Password = universityPassword,
                    CreatorId = developerEmail
                };
                await campusBridgeDbContext.Universities.AddAsync(universityData);
                await campusBridgeDbContext.SaveChangesAsync();
            }


            string collegeEmail = "college@gmail.com";
            string collegePassword = "College@123";
            if (userManager.Users.All(u => u.Email != collegeEmail))
            {
                var collegeUser = new IdentityUser
                {
                    UserName = collegeEmail,
                    Email = collegeEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(collegeUser, collegePassword);
                await userManager.AddToRoleAsync(collegeUser, "College");
            }

            string teacherEmail = "teacher@gmail.com";
            string teacherPassword = "Teacher@123";
            if (userManager.Users.All(u => u.Email != teacherEmail))
            {
                var teacherUser = new IdentityUser
                {
                    UserName = teacherEmail,
                    Email = teacherEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(teacherUser, teacherPassword);
                await userManager.AddToRoleAsync(teacherUser, "Teacher");
            }
            string studentEmail = "student@gmail.com";
            string studentPassword = "Student@123";
            if (userManager.Users.All(u => u.Email != studentEmail))
            {
                var studentUser = new IdentityUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(studentUser, studentPassword);
                await userManager.AddToRoleAsync(studentUser, "Student");
            }

        }
    }
}
