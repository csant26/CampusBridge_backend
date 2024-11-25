using backend.Data;
using backend.Models.Domain.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository.Students
{
    public class ClubRepository : IClubRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public ClubRepository(CampusBridgeDbContext campusBridgeDbContext, UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<Club> CreateClub(Club club)
        {
            //Approach 1
            var userCheck = await userManager.FindByEmailAsync(club.ClubHeadId);
            var roles = await userManager.GetRolesAsync(userCheck);
            if (!roles.Contains("ClubHead")) { return null; }
            //Approach 2
            //var student = await campusBridgeDbContext.Students
            //    .FirstOrDefaultAsync(x => x.StudentId == club.ClubHeadId);
            //if (student == null) { return null; }
            //if (student.isClubHead == false) { return null; }
            
            var students = await campusBridgeDbContext.Students
                .Where(x=>club.ClubId.Contains(x.StudentId))
                .ToListAsync();
            if (students == null) { return null; }
            foreach(var clubStudent in students)
            {
                var existingUser = await userManager.FindByEmailAsync(clubStudent.Email);
                if (existingUser == null) { return null; }
                await userManager.AddToRoleAsync(existingUser, "ClubMember");
            }
            club.Students = students;
            await campusBridgeDbContext.Clubs.AddAsync(club);
            await campusBridgeDbContext.SaveChangesAsync();
            return club;
        }
        public async Task<List<Club>> GetClub()
        {
            var clubs = await campusBridgeDbContext.Clubs.Include(x => x.Students).ToListAsync();
            if (clubs == null) { return null; }
            return clubs;
        }
        public async Task<Club> GetClubById(string ClubId)
        {
            var club = await campusBridgeDbContext.Clubs
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.ClubId == ClubId);
            if (club == null) { return null; }
            return club;
        }
        public async Task<Club> UpdateClub(string ClubId, Club club)
        {
            var exisitingClub = await GetClubById(ClubId);
            if (exisitingClub == null) { return null; }
            if(exisitingClub.ClubHeadId!= club.ClubHeadId) { return null; }

            exisitingClub.Name = club.Name;
            exisitingClub.Description = club.Description;

            foreach(var member in exisitingClub.Students)
            {
                var existingClubUser = await userManager.FindByEmailAsync(member.StudentId);
                if (existingClubUser == null) { return null; }
                await userManager.RemoveFromRoleAsync(existingClubUser, "ClubMember");
            }

            var students = await campusBridgeDbContext.Students
                .Where(x => club.ClubId.Contains(x.StudentId))
                .ToListAsync();
            foreach (var clubStudent in students)
            {
                var existingUser = await userManager.FindByEmailAsync(clubStudent.Email);
                if (existingUser == null) { return null; }
                await userManager.AddToRoleAsync(existingUser, "ClubMember");
            }
            exisitingClub.Students = students;
            await campusBridgeDbContext.SaveChangesAsync();
            return exisitingClub;

        }
        public async Task<Club> DeleteClub(string ClubId, string ClubHeadId)
        {
            var exisitingClub = await GetClubById(ClubId);
            if (exisitingClub == null) { return null; }
            if (exisitingClub.ClubHeadId != ClubHeadId) { return null; }
            foreach (var member in exisitingClub.Students)
            {
                var existingClubUser = await userManager.FindByEmailAsync(member.StudentId);
                if (existingClubUser == null) { return null; }
                await userManager.RemoveFromRoleAsync(existingClubUser, "ClubMember");
            }
            campusBridgeDbContext.Clubs.Remove(exisitingClub);
            await campusBridgeDbContext.SaveChangesAsync();
            return exisitingClub;
        }

    }
}
