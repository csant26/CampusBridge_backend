using System.Text.Json.Serialization;

namespace backend.Models.Domain.Content.Syllabi
{
    public class Unit
    {
        public string UnitId { get; set; }
        public string Title { get; set; }
        public int CompletionHours { get; set; }
        public List<string> SubUnits { get; set; }
        public string CourseId {  get; set; } //foreign key
        [JsonIgnore]
        public Course Course { get; set; } //one-to-one
    }
}
