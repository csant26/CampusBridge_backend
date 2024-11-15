using backend.Models.Domain.Content.Images;

namespace backend.Repository.Content
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
