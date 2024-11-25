using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Teachers;
using backend.Models.DTO.Content.Syllabus;
using backend.Models.DTO.Teacher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Assignment
{
    public class AssignmentDTO
    {
        public string AssignmentId { get; set; }
        public string Question { get; set; }
        public string? FilePath { get; set; } = null;
        public CourseDTO CourseDTO { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public TeacherDTO TeacherDTO { get; set; }
        public List<SubmissionDTO> SubmissionDTO {  get; set; }
    }
}
