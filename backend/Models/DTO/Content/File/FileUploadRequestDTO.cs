using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.File
{
    public class FileUploadRequestDTO
    {
        [Required]
        public string FileId {  get; set; }
        [Required]
        public IFormFile FileToUpload { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
