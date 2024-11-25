using backend.Models.Domain.Content.Files;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface IFileRepository
    {
        Task<FileDomain> UploadFile(FileDomain file);
    }
}
