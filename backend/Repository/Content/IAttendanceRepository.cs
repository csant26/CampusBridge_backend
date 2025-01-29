using backend.Models.Domain.Content.Attendances;
using backend.Models.DTO.Content.Attendances;

namespace backend.Repository.Content
{
    public interface IAttendanceRepository
    {
        Task<Attendance> CreateAttendance(Attendance attendance, AddAttendanceDTO addAttendanceDTO);
        Task<List<Attendance>> GetAttendance();
        Task<List<StudentAttendanceData>> GetStudentAttendance(string StudentId);
    }
}
