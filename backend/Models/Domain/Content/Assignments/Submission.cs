﻿using backend.Models.Domain.Students;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Assignments
{
    public class Submission
    {
        public string SubmissionId { get; set; }
        public string Answer {  get; set; }
        public string? FilePath { get; set; } = null;
        public string? Score { get; set; }
        public string StudentId { get; set; } //foreign key
        [JsonIgnore]
        public Student Student { get; set; } //one-to-one
        public string AssignmentId {  get; set; } //foreign key
        [JsonIgnore]
        public Assignment Assignment { get; set; } //one-to-one

    }
}
