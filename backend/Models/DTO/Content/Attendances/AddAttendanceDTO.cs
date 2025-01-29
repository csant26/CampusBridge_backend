using backend.Models.DTO.Student;

namespace backend.Models.DTO.Content.Attendances
{
    public class AddAttendanceDTO
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public Dictionary<string, bool> StudentPresence { get; set; }

    }
}
