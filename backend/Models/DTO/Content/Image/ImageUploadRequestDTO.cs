using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Images
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public string FileId {  get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
