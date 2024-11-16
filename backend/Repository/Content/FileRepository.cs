using backend.Data;
using backend.Models.Domain.Content.Files;
namespace backend.Repository.Content
{
    public class FileRepository : IFileRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileRepository(CampusBridgeDbContext campusBridgeDbContext
            ,IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<FileDomain> UploadFile(FileDomain fileDomain)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Files",
                $"{fileDomain.FileName}{fileDomain.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await fileDomain.FileToUpload.CopyToAsync(stream);

            //URL path to access the image
            //https://localhost:1234/files/abc.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Files/{fileDomain.FileName}{fileDomain.FileExtension}";


            fileDomain.FilePath = urlFilePath;

            await campusBridgeDbContext.Files.AddAsync(fileDomain);
            await campusBridgeDbContext.SaveChangesAsync();

            return fileDomain;
        }
    }
}
