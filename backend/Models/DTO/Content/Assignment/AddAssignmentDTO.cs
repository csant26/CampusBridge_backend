using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Teachers;
using backend.Models.DTO.Content.Syllabus;
using backend.Models.DTO.Teacher;
using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.Content.Assignment
{
    public class AddAssignmentDTO
    {
        [Required]
        public string AssignmentId { get; set; }
        [Required]
        [MinLength(3)]
        public string Question { get; set; }
        [Required]
        public string CourseId {  get; set; }
        [Required]
        public DateTime AssignedDate { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        [Required]
        public string TeacherId { get; set; }
    }
}
