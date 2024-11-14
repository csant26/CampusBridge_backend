using backend.Models.Domain.TeacherDomain;

namespace backend.Models.Domain.Content.Assignment
{
    public class Assignment
    {
        public string AssignmentId { get; set; }
        public string Question {  get; set; }
        public string Subject {  get; set; }
        public DateTime AssignedDate {  get; set; }
        public DateTime SubmissionDate { get; set; }
        public string TeacherId {  get; set; }
        public Teacher Teacher {  get; set; }
    }
}
