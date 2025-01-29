using backend.Models.Domain.Content.Results;
using backend.Models.Domain.Students;
using backend.Models.DTO.College;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Help;
using backend.Models.DTO.Content.Result;
using backend.Models.DTO.Content.Syllabus;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Student
{
    public class StudentDTO
    {
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Gender { get; set; }
        public bool? isClubHead { get; set; } = false;
        public bool? isAuthor { get; set; } = false;
        //Navigation Properties
        public AcademicDTO AcademicDTO { get; set; }
        public FinancialDTO FinancialDTO { get; set; }
        public List<CourseDTO> CourseDTO {  get; set; }
        [JsonIgnore]
        public List<ClubDTO> ClubsDTO { get; set; }
        [JsonIgnore]
        public List<SubmissionDTO> SubmissionDTO { get; set; }
        [JsonIgnore]
        public List<QuestionDTO> QuestionDTO { get; set; }
        //[JsonIgnore]  
        public CollegeDTO CollegeDTO {  get; set; }
        [JsonIgnore]
        public List<ResultDTO> ResultDTO {  get; set; }
        [JsonIgnore]
        public List<StudentAttendance> StudentAttendances { get; set; }
    }
}
