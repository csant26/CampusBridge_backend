using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models.Domain.Students
{
    public class Club
    {
        public string ClubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClubHeadId {  get; set; }
        [JsonIgnore] //to avoid circular references
        public List<Student> Students { get; set; } //one-to-many
    }
}
