using backend.Models.Domain.Content.Files;

namespace backend.Repository.Content
{
    public interface IFileRepository
    {
        Task<FileDomain> UploadFile(FileDomain file);
    }
}
