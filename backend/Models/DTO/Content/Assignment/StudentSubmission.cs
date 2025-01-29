namespace backend.Models.DTO.Content.Assignment
{
    public class StudentSubmission
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string SubmissionId { get; set; }
        public string CourseName { get; set; }
        public string AssignmentFilePath { get; set; }
        public string SubmissionFilePath { get; set; }
    }
}
