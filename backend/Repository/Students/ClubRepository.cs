using backend.Data;
using backend.Models.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Students
{
    public class ClubRepository : IClubRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public ClubRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<Club> CreateClub(Club club)
        {
            var student = await campusBridgeDbContext.Students
                .FirstOrDefaultAsync(x => x.StudentId == club.ClubHeadId);
            if (student == null) { return null; }
            if (student.isClubHead == false) { return null; }
            var students = await campusBridgeDbContext.Students
                .Where(x=>club.ClubId.Contains(x.StudentId))
                .ToListAsync();
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
            var students = await campusBridgeDbContext.Students
                .Where(x => club.ClubId.Contains(x.StudentId))
                .ToListAsync();
            exisitingClub.Students = students;
            await campusBridgeDbContext.SaveChangesAsync();
            return exisitingClub;

        }
        public async Task<Club> DeleteClub(string ClubId, string ClubHeadId)
        {
            var exisitingClub = await GetClubById(ClubId);
            if (exisitingClub == null) { return null; }
            if (exisitingClub.ClubHeadId != ClubHeadId) { return null; }
            campusBridgeDbContext.Clubs.Remove(exisitingClub);
            await campusBridgeDbContext.SaveChangesAsync();
            return exisitingClub;
        }

    }
}
