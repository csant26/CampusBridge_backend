using backend.Models.Domain.Universities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Universities
{
    public interface IUniversityRepository
    {
        Task<University> CreateUniversity(University university);
        Task<List<University>> GetUniversity();
        Task<University> GetUniversityById(string UniversityId);
        Task<University> UpdateUniversity(string UniversityId, University university);
        Task<University> DeleteUniversity(string UniversityId, string DeveloperId);
    }
}
