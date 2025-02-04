using backend.Data;
using backend.Models.Domain.Universities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Universities
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UniversityRepository(CampusBridgeDbContext campusBridgeDbContext, 
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<University> CreateUniversity(University universityToAdd)
        {
            var developerUser = await userManager.FindByEmailAsync(universityToAdd.CreatorId);
            if (developerUser == null) { return null; }
            var roles = await userManager.GetRolesAsync(developerUser);
            if (!roles.Contains("Developer")) { return null; }

            //Register in the authenticated users
            if (userManager.Users.All(u => u.Email != universityToAdd.Email))
            {
                var universityUser = new IdentityUser
                {
                    UserName = universityToAdd.Email,
                    Email = universityToAdd.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(universityUser, universityToAdd.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(universityUser, "University");
                    await userManager.AddToRoleAsync(universityUser, "Author");

                    await campusBridgeDbContext.Universities.AddAsync(universityToAdd);
                    await campusBridgeDbContext.SaveChangesAsync();
                    return universityToAdd;
                }
            }
            return null;

        }

        public async Task<List<University>> GetUniversity()
        {
            var universities = await campusBridgeDbContext.Universities
                .Include(x=>x.Colleges)
                .ToListAsync();
            if (universities == null) { return null; }
            return universities;
        }

        public async Task<University> GetUniversityById(string UniversityId)
        {
            var university = await campusBridgeDbContext.Universities
                .Include(x => x.Colleges)
                .FirstOrDefaultAsync(x=>x.UniversityId==UniversityId);
            if (university == null) { return null; }
            return university;
        }

        public async Task<University> UpdateUniversity(string UniversityId, University university)
        {
            var existingUniversity = await GetUniversityById(UniversityId);
            if (existingUniversity == null) { return null; }
            var developerUser = await userManager.FindByEmailAsync(university.CreatorId);
            if (developerUser == null) { return null; }
            var roles = await userManager.GetRolesAsync(developerUser);
            if ( !roles.Contains("Developer") && 
                existingUniversity.UniversityId != university.CreatorId ) { return null; }

            var existingUniversityUser = await userManager.FindByEmailAsync(existingUniversity.Email);
            if(existingUniversityUser == null) { return null; }
            await userManager.DeleteAsync(existingUniversityUser);

            existingUniversity.Name = university.Name;
            existingUniversity.Description = university.Description;
            if (existingUniversity.UniversityId == university.CreatorId)
            {
                existingUniversity.Email = university.Email;
                existingUniversity.Password = university.Password;
            }

            await campusBridgeDbContext.SaveChangesAsync();

            if(userManager.Users.All(u=>u.Email!=existingUniversityUser.Email))
            {
                var newUniversityUser = new IdentityUser
                {
                    UserName = existingUniversity.Email,
                    Email = existingUniversity.Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newUniversityUser, existingUniversity.Password);
                await userManager.AddToRoleAsync(newUniversityUser, "University");
                await userManager.AddToRoleAsync(newUniversityUser, "Author");
                return existingUniversity;
            }
            else { return null; }
        }

        public async Task<University> DeleteUniversity(string UniversityId, string DeveloperId)
        {
            var existingUniversity = await GetUniversityById(UniversityId);
            if (existingUniversity == null) { return null; }
            var developerUser = await userManager.FindByEmailAsync(DeveloperId);
            if (developerUser == null) { return null; }
            var roles = await userManager.GetRolesAsync(developerUser);
            if (!roles.Contains("Developer")) { return null; }

            var existingUniversityUser = await userManager.FindByEmailAsync(existingUniversity.Email);
            if (existingUniversityUser == null) { return null; }
            await userManager.DeleteAsync(existingUniversityUser);

            campusBridgeDbContext.Universities.Remove(existingUniversity);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingUniversity;
        }
    }
}
