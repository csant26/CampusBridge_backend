using backend.Data;
using backend.Models.Domain.Colleges;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Colleges
{
    public class CollegeRepository : ICollegeRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public CollegeRepository(CampusBridgeDbContext campusBridgeDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<College> CreateCollege(College collegeToAdd)
        {
            //Approach 1: Rolebased
            //var universityUser = await userManager.FindByEmailAsync(collegeToAdd.UniversityId);
            //if (universityUser == null) { return null; }
            //var roles = await userManager.GetRolesAsync(universityUser);
            //if (!roles.Contains("University")) { return null; }

            //Approach 2: Id based
            var university = await campusBridgeDbContext
                .Universities.FindAsync(collegeToAdd.UniversityId);
            if (university == null) { return null; }
            collegeToAdd.University = university;

            //Register in the authenticated users
            if (userManager.Users.All(u => u.Email != collegeToAdd.Email))
            {
                var collegeUser = new IdentityUser
                {
                    UserName = collegeToAdd.Email,
                    Email = collegeToAdd.Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(collegeUser, collegeToAdd.Password);
                await userManager.AddToRoleAsync(collegeUser, "College");
                await userManager.AddToRoleAsync(collegeUser, "Author");
            }
            else { return null; }

            await campusBridgeDbContext.Colleges.AddAsync(collegeToAdd);
            await campusBridgeDbContext.SaveChangesAsync();
            return collegeToAdd;
        }

        public async Task<List<College>> GetCollege()
        {
            var colleges = await campusBridgeDbContext.Colleges
                .Include(x=>x.Students).Include(u=>u.University).Include(t=>t.Teachers)
                .ToListAsync();
            if (colleges == null) { return null; }
            return colleges;
        }

        public async Task<College> GetCollegeById(string CollegeId)
        {
            var college = await campusBridgeDbContext.Colleges
                .Include(x => x.Students).Include(u => u.University).Include(t => t.Teachers)
                .FirstOrDefaultAsync(f => f.CollegeId == CollegeId);
            if (college == null) { return null; }
            return college;
        }

        public async Task<College> UpdateCollege(string CollegeId, College college)
        {
            var existingCollege = await GetCollegeById(CollegeId);
            if (existingCollege == null) { return null; }
            var existingUniversity = await campusBridgeDbContext.Universities.FindAsync(college.UniversityId);
            if (existingUniversity == null && existingCollege.CollegeId != college.UniversityId)
            { return null; }

            var existingCollegeUser = await userManager.FindByEmailAsync(existingCollege.Email);
            await userManager.DeleteAsync(existingCollegeUser);

            existingCollege.Name = college.Name;
            existingCollege.Location = college.Location;
            existingCollege.Phone = college.Phone;
            existingCollege.Description = college.Description;

            if (existingCollege.CollegeId == college.UniversityId)
            {
                existingCollege.Email = college.Email;
                existingCollege.Password = college.Password;
            }
            await campusBridgeDbContext.SaveChangesAsync();

            if (userManager.Users.All(u => u.Email != existingCollege.Email))
            {
                var newCollegeUser = new IdentityUser
                {
                    UserName = existingCollege.Email,
                    Email = existingCollege.Email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newCollegeUser, existingCollege.Password);
                await userManager.AddToRoleAsync(newCollegeUser, "College");
                await userManager.AddToRoleAsync(newCollegeUser, "Author");
                return existingCollege;
            }
            else { return null; }
        }
        public async Task<College> DeleteCollege(string CollegeId, string UniversityId)
        {
            var existingCollege = await GetCollegeById(CollegeId);
            if (existingCollege == null) { return null; }
            var existingUniversityUser = await userManager.FindByEmailAsync(UniversityId);
            if (existingUniversityUser == null) { return null; }
            var roles = await userManager.GetRolesAsync(existingUniversityUser);
            if (!roles.Contains("University")) { return null; }

            var existingCollegeUser = await userManager.FindByEmailAsync(CollegeId);
            if (existingCollegeUser == null) { return null; }
            await userManager.DeleteAsync(existingCollegeUser);

            campusBridgeDbContext.Colleges.Remove(existingCollege);
            await campusBridgeDbContext.SaveChangesAsync();

            return existingCollege;

        }
    }
}
