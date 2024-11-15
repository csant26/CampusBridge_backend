using backend.Models.Domain.Content.Images;
using backend.Models.DTO.Content.Images;
using backend.Repository.Content;
using Microsoft.AspNetCore.Mvc;

namespace backend.Images
{
    public class ImageHandling
    {
        private readonly IImageRepository imageRepository;

        public ImageHandling(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public async Task<string> UploadImage(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            if(ValidateFileUpload(imageUploadRequestDTO))
            {
                var image = new Image
                {
                    ImageId = imageUploadRequestDTO.FileId,
                    File = imageUploadRequestDTO.File,
                    FileName = imageUploadRequestDTO.FileName,
                    FileDescription = imageUploadRequestDTO.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileSizeInBytes = imageUploadRequestDTO.File.Length,
                };
                var uploadedImage = await imageRepository.UploadImage(image);
                if (uploadedImage == null) { return null; }
                return uploadedImage.FilePath;
            }
            return null;
        }
        private bool ValidateFileUpload(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDTO.File.FileName).ToLower()))
            {
                return false;
            }
            if (imageUploadRequestDTO.File.Length > 10485760)
            {
                return false;
            }
            return true;
        }
    }
}
