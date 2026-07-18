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

        await SeedUser(userManager, roleManager, "Seed:University", "University");
        await SeedUser(userManager, roleManager, "Seed:College", "College");
        await SeedUser(userManager, roleManager, "Seed:Teacher", "Teacher");
        await SeedUser(userManager, roleManager, "Seed:Student", "Student");

        var universityEmail = configuration["Seed:University:Email"];
        var universityPassword = configuration["Seed:University:Password"];
        if (!string.IsNullOrWhiteSpace(universityEmail) &&
            !string.IsNullOrWhiteSpace(universityPassword) &&
            await campusBridgeDbContext.Universities.FindAsync(universityEmail) is null)
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

    private async Task SeedUser(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        string configPrefix,
        string role)
    {
        var email = configuration[$"{configPrefix}:Email"];
        var password = configuration[$"{configPrefix}:Password"];
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        if (userManager.Users.All(u => u.Email != email))
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
