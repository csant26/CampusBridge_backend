using System.Text.Json.Serialization;

namespace backend.Models.Domain.Student
{
    public class Club
    {
        public string ClubId { get; set; }
        public string Name { get; set; }
        [JsonIgnore] //to avoid circular references
        public List<Student> Students { get; set; } //one-to-many
    }
}
