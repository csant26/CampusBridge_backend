﻿using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Syllabi;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Teachers
{
    public class Teacher
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public List<Course> Courses {  get; set; } //one-to-many
        public List<Assignment>? Assignments { get; set; } = null; //one-to-many
        //subject, positions, working hours
    }
}
