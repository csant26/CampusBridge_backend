using backend.Data;
using backend.Models.Domain.Content.Images;
using backend.Models.DTO.Content.Images;

namespace backend.Repository.Content
{
    public class ImageRepository : IImageRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageRepository(CampusBridgeDbContext campusBridgeDbContext
            ,IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Images",
                $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //URL path to access the image
            //https://localhost:1234/images/abc.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Image/{image.FileName}{image.FileExtension}";


            image.FilePath = urlFilePath;

            await campusBridgeDbContext.Images.AddAsync(image);
            await campusBridgeDbContext.SaveChangesAsync();

            return image;
        }
    }
}
