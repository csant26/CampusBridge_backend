using backend.Models.Domain.Content.Schedules;

namespace backend.Repository.Content
{
    public interface IScheduleRepository
    {
        Task<Schedule> CreateSchedule(Schedule schedule);
        Task<List<Schedule>> GetScheduleByRole(string Role);
        Task<List<Schedule>> GetScheduleByCategory(string Category);
        Task<Schedule> CreateExamSchedule(ExamSchedule examSchedule);
        Task<Schedule> CreateTeacherSchedule(TeacherSchedule schedule);
        Task<Schedule> CreateStudentSchedule(Schedule schedule);

    }
}
