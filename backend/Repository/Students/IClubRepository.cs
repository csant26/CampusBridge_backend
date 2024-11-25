using backend.Models.Domain.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Students
{
    public interface IClubRepository
    {
        Task<Club> CreateClub(Club club);
        Task<List<Club>> GetClub();
        Task<Club> GetClubById(string ClubId);
        Task<Club> UpdateClub(string ClubId, Club club);
        Task<Club> DeleteClub(string ClubId, string ClubHeadId);
    }
}
