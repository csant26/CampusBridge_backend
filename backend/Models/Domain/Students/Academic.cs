using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Students
{
    public class Academic
    {
        public string AcademicId { get; set; }
        public string Batch { get; set; }
        public string Semester { get; set; }
        public string Faculty { get; set; }
        [JsonIgnore] //to avoid circular references
        public List<Student> Students {  get; set; } //one-to-many
    }
}
