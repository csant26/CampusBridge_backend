using backend.Models.Domain.Content.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface IResultRepository
    {
        Task<Result> CreateResult(Result result);
        Task<List<Result>> GetResult();
        Task<Result> GetResultById(string ResultId);
        Task<Result> UpdateResult(string ResultId, Result result);
        Task<Result> DeleteResult(string ResultId);
    }
}
