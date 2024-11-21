using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using backend.Models.Domain.Universities;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Colleges
{
    public class College
    {
        public string CollegeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description {  get; set; }
        public List<Student> Students { get; set; } //one-to-many
        [JsonIgnore]
        public List<Teacher> Teachers {  get; set; } //one-to-many
        public string UniversityId { get; set; } //foreign key
        [JsonIgnore]
        public University University { get; set; } //one-to-one
    }
}
