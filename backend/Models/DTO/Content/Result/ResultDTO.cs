using backend.Models.DTO.Student;

namespace backend.Models.DTO.Content.Result
{
    public class ResultDTO
    {
        public string ResultId { get; set; }
        public string ExaminationType { get; set; }
        public string Semester { get; set; }
        public string Percentage { get; set; }
        public string Status { get; set; } //Pass or fail
        public StudentDTO StudentDTO { get; set; }
    }
}
