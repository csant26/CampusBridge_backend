using backend.Data;
using backend.Models.Domain.Universities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class SeedData
{
    private readonly CampusBridgeDbContext campusBridgeDbContext;
    private readonly IConfiguration configuration;

    public SeedData(CampusBridgeDbContext campusBridgeDbContext, IConfiguration configuration)
    {
        this.campusBridgeDbContext = campusBridgeDbContext;
        this.configuration = configuration;
    }

    public async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames =
        {
            "Developer", "University", "College", "Teacher",
            "Student", "ClubHead", "Author", "ClubMember"
        };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var developerEmail = configuration["Seed:Developer:Email"];
        var developerPassword = configuration["Seed:Developer:Password"];
        if (string.IsNullOrWhiteSpace(developerEmail) || string.IsNullOrWhiteSpace(developerPassword))
        {
            return;
        }

        if (await userManager.FindByEmailAsync(developerEmail) is null)
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

        var universityEmail = configuration["Seed:University:Email"];
        var universityPassword = configuration["Seed:University:Password"];
        if (!string.IsNullOrWhiteSpace(universityEmail) && !string.IsNullOrWhiteSpace(universityPassword))
        {
            if (await userManager.FindByEmailAsync(universityEmail) is null)
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

            if (await campusBridgeDbContext.Universities.FindAsync(universityEmail) is null)
            {
                await campusBridgeDbContext.Universities.AddAsync(new University
                {
                    UniversityId = universityEmail,
                    Name = universityEmail,
                    Email = universityEmail,
                    Description = universityEmail,
                    Password = universityPassword,
                    CreatorId = developerEmail
                });
                await campusBridgeDbContext.SaveChangesAsync();
            }
        }

        var collegeEmail = configuration["Seed:College:Email"];
        var collegePassword = configuration["Seed:College:Password"];
        if (!string.IsNullOrWhiteSpace(collegeEmail) &&
            !string.IsNullOrWhiteSpace(collegePassword) &&
            await userManager.FindByEmailAsync(collegeEmail) is null)
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

        var teacherEmail = configuration["Seed:Teacher:Email"];
        var teacherPassword = configuration["Seed:Teacher:Password"];
        if (!string.IsNullOrWhiteSpace(teacherEmail) &&
            !string.IsNullOrWhiteSpace(teacherPassword) &&
            await userManager.FindByEmailAsync(teacherEmail) is null)
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

        var studentEmail = configuration["Seed:Student:Email"];
        var studentPassword = configuration["Seed:Student:Password"];
        if (!string.IsNullOrWhiteSpace(studentEmail) &&
            !string.IsNullOrWhiteSpace(studentPassword) &&
            await userManager.FindByEmailAsync(studentEmail) is null)
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
