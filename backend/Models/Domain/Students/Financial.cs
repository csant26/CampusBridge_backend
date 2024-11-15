using System.Text.Json.Serialization;

namespace backend.Models.Domain.Students
{
    public class Financial
    {
        public string FinancialId { get; set; }
        public bool FeePaid { get; set; }
        public decimal Fee { get; set; }
        public decimal Scholarship { get; set; }=0;
        [JsonIgnore] //to avoid circular references
        public List<Student> Students { get; set; } //one-to-many
    }
}
