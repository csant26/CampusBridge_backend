using backend.Models.Domain.Students;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Domain.Content.Attendances
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        [NotMapped]
        public Dictionary<Student,bool> StudentPresence { get; set; }
    }
}
