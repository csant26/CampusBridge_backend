using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Colleges
{
    public class College
    {
        public int CollegeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password {  get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description {  get; set; }
        public List<Student> Students { get; set; } //one-to-many
        [JsonIgnore]
        public List<Teacher> Teachers {  get; set; } //one-to-many
    }
}
