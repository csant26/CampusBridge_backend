using backend.Models.Domain.Content.Assignments;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Files
{
    public class FileDomain
    {
        public string FileId { get; set; }
        [NotMapped]
        public IFormFile FileToUpload { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
