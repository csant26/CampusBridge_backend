using backend.Models.Domain.Content.Image;

namespace backend.Repository.Content
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
