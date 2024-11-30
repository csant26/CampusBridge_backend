using backend.Models.Domain.Students;
using System.Collections.Generic;

namespace backend.Models.Domain.Content.Syllabi
{
    public class Syllabus
    {
        public string SyllabusId { get; set; }
        public List<Course> Courses { get; set; } //one-to-many
        public string Semester { get; set; }
        public string AllowedElectiveNo { get; set; }
    }
}
