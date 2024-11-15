using backend.CustomActionFilter;
using backend.Models.Domain.Content.Images;
using backend.Models.DTO.Content.Images;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ValidateModel]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)
        {
            ValidateFileUpload(imageUploadRequestDTO);
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
            if(uploadedImage == null) { return BadRequest("Image couldn't be uploaded."); }
            return Ok(uploadedImage);
        }
        private void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDTO.File.FileName).ToLower()))
            {
                ModelState.AddModelError("File", "Unsupported File Extension.");
            }
            if (imageUploadRequestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size more than 10 MB.");
            }
        }
         
    }
}
