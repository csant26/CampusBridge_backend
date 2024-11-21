﻿namespace backend.Models.DTO.College
{
    public class UpdateCollegeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string CreatorId {  get; set; } //Creator: College or University
    }
}
