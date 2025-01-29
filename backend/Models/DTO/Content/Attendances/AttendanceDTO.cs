using backend.Models.Domain.Students;
using backend.Models.DTO.Student;

namespace backend.Models.DTO.Content.Attendances
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string StudentPresenceJson { get; set; }
        public Dictionary<StudentDTO, bool> StudentPresence { get; set; }
    }
}
