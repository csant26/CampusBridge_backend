using backend.Models.Domain.Students;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Domain.Content.Attendances
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string StudentPresenceJson { get; set; }
        [NotMapped]
        public Dictionary<string,bool> StudentPresence { get; set; }
    }
}
