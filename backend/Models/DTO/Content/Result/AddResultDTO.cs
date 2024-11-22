namespace backend.Models.DTO.Content.Result
{
    public class AddResultDTO
    {
        public string ResultId { get; set; }
        public string ExaminationType { get; set; }
        public string Semester { get; set; }
        public string Percentage { get; set; }
        public string Status { get; set; } //Pass or fail
        public string StudentId { get; set; }
    }
}
