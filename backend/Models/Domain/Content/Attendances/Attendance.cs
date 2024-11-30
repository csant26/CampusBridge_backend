using backend.Models.Domain.Students;

namespace backend.Models.Domain.Content.Attendances
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public Dictionary<Student,bool> StudentPresence { get; set; }
    }
}
