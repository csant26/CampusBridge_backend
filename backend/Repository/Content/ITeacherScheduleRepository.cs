using backend.Models.Domain.Content.Schedules;

namespace backend.Repository.Content
{
    public interface ITeacherScheduleRepository
    {
        Task<Schedule> CreateTeacherSchedule(TeacherSchedule teacherScheduleData);
    }
}
