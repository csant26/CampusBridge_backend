using backend.Models.Domain.Content.Syllabus;
using System.Text.Json.Serialization;

namespace backend.Models.DTO.Content.Syllabus
{
    public class UnitDTO
    {
        public string UnitId { get; set; }
        public string Title { get; set; }
        public int CompletionHours { get; set; }
        public List<string> SubUnits { get; set; }
    }
}
