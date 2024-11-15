﻿using backend.Models.Domain.Students;
using backend.Models.DTO.Content.Syllabus;

namespace backend.Models.DTO.Student
{
    public class StudentDTO
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        //Navigation Properties
        public AcademicDTO AcademicDTO { get; set; }
        public FinancialDTO FinancialDTO { get; set; }
        public List<CourseDTO> CourseDTO {  get; set; }
        public List<ClubDTO>? ClubsDTO { get; set; }
        
    }
}
