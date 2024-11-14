﻿using backend.Models.Domain.Content.Syllabus;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Syllabus
{
    public class CourseDTO
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string CourseObjective { get; set; }
        public string FullMarks { get; set; }
        public string PassMarks { get; set; }
        public int CreditHour { get; set; }
        public string LabDescription { get; set; }
        public List<string> Books { get; set; }
        public List<UnitDTO> UnitsDTO { get; set; }
    }
}
