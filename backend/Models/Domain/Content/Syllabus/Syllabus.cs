namespace backend.Models.Domain.Content.Syllabus
{
    public class Syllabus
    {
        public string SyllabusId { get; set; }
        public List<Course> Courses { get; set; } //one-to-many
        public string Semester { get; set; }
    }
}
