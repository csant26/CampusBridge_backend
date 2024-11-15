﻿using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Syllabi;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Students
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        //public string SymbolNo { get; set; }
        //public string RegistrationNo {  get; set; }
        //Foreign Keys
        public string AcademicId { get; set; }
        public string FinancialId { get; set; }
        //Navigation Properties
        public Academic Academic { get; set; } //one-to-one
        public Financial Financial { get; set; } //one-to-one
        public List<Club>? Clubs { get; set; } = null; //one-to-many
        public List<Submission>? Submissions { get; set; } = null; //one-to-many
        [JsonIgnore]
        public List<Course> Courses { get; set; } //one-to-many
    }
}
