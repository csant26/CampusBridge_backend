using System.Text.Json.Serialization;

namespace backend.Models.Domain.Student
{
    public class Academic
    {
        public string AcademicId { get; set; }
        public int Batch { get; set; }
        public string Faculty { get; set; }
        [JsonIgnore] //to avoid circular references
        public List<Student> Students {  get; set; } //one-to-many
    }
}
