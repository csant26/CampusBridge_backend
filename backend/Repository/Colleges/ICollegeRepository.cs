using backend.Models.Domain.Colleges;

namespace backend.Repository.Colleges
{
    public interface ICollegeRepository
    {
        Task<College> CreateCollege(College college);
        Task<List<College>> GetCollege();
        Task<College> GetCollegeById(string CollegeId);
        Task<College> UpdateCollege(string CollegeId, College college);
        Task<College> DeleteCollege(string CollegeId, string UniversityId);

    }
}
