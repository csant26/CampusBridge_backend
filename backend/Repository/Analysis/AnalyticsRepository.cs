using backend.Data;
using backend.Models.DTO.Analytics;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Analysis
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public AnalyticsRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<GenderInfoDTO> GetGenderData()
        {
            var maleData = await campusBridgeDbContext.Students.CountAsync(x => x.Gender == "Male");
            var femaleData = await campusBridgeDbContext.Students.CountAsync(x => x.Gender == "Female");
            return new GenderInfoDTO()
            {
                MaleNo = Convert.ToString(maleData),
                FemaleNo = Convert.ToString(femaleData)

            };
        }

        public async Task<ResultInfoDTO> GetGraduateData()
        {
            return new ResultInfoDTO();
        }

        public async Task<ResultInfoDTO> GetResultData()
        {
            var passResult = await campusBridgeDbContext.Results.CountAsync(x => x.Status == "Pass");
            var failResult = await campusBridgeDbContext.Results.CountAsync(x => x.Status == "Fail");
            return new ResultInfoDTO()
            {
                PassNo = Convert.ToString(passResult),
                FailNo = Convert.ToString(failResult)
            };
        }
    }
}
