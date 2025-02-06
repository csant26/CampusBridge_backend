using backend.Models.Domain.Content.Schedules;
using backend.Models.DTO.Content.Schedule;

namespace backend.Repository.Content
{
    public interface ITeacherScheduleRepository
    {
        //Task<Schedule> CreateTeacherSchedule(TeacherSchedule teacherScheduleData);
        Task<List<TeacherScheduleResponse>> CreateTeacherScheduleFromGraph(List<ClassSession> sessions, List<AddTeacherScheduleRequest> teacherScheduleRequest);
    }
}
