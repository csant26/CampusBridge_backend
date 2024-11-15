﻿using backend.Models.Domain.Content.Assignments;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Images
{
    public class Image
    {
        public string ImageId { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

        public string? AssignmentId { get; set; } = null;
        [JsonIgnore]
        public Assignment? Assignment { get; set; } = null; //one-to-one
        public string? SubmissionId {  get; set; } = null;
        [JsonIgnore]
        public Submission? Submission { get; set; } = null; //one-to-one
    }
}
