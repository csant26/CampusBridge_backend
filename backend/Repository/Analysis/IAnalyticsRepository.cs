using backend.Models.DTO.Analytics;

namespace backend.Repository.Analysis
{
    public interface IAnalyticsRepository
    {
        public Task<GenderInfoDTO> GetGenderData();
        public Task<ResultInfoDTO> GetResultData();
        public Task<ResultInfoDTO> GetGraduateData();
    }
}
