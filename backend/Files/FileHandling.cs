using backend.Models.Domain.Content.Files;
using backend.Models.DTO.Content.File;
using backend.Repository.Content;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Files
{
    public class FileHandling
    {
        private readonly IFileRepository fileRepository;

        public FileHandling(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }
        public async Task<string> UploadFile(FileUploadRequestDTO fileUploadRequestDTO)
        {
            if(ValidateFileUpload(fileUploadRequestDTO))
            {
                var fileDomain = new FileDomain
                {
                    FileId = fileUploadRequestDTO.FileId,
                    FileToUpload = fileUploadRequestDTO.FileToUpload,
                    FileName = fileUploadRequestDTO.FileName,
                    FileDescription = fileUploadRequestDTO.FileDescription,
                    FileExtension = Path.GetExtension(fileUploadRequestDTO.FileToUpload.FileName),
                    FileSizeInBytes = fileUploadRequestDTO.FileToUpload.Length,
                };
                var uploadedFile = await fileRepository.UploadFile(fileDomain);
                if (uploadedFile == null) { return null; }
                return uploadedFile.FilePath;
            }
            return null;
        }
        private bool ValidateFileUpload(FileUploadRequestDTO fileUploadRequestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };

            if (!allowedExtensions.Contains(Path.GetExtension(fileUploadRequestDTO.FileToUpload.FileName).ToLower()))
            {
                return false;
            }
            if (fileUploadRequestDTO.FileToUpload.Length > 10485760)
            {
                return false;
            }
            return true;
        }
    }
}
