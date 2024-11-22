using backend.Models.Domain.Students;

namespace backend.Models.Domain.Content.Results
{
    public class Result
    {
        public string ResultId {  get; set; }
        public string ExaminationType { get; set; }
        public string Semester {  get; set; }
        public string Percentage {  get; set; }
        public string Status { get; set; } //Pass or fail
        public string StudentId { get; set; }//foreign key
        public Student Student { get; set; } //one-to-one

    }
}
