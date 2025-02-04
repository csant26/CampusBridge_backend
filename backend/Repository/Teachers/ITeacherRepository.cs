using backend.Models.Domain.Teachers;
using backend.Models.DTO.Content.Schedule;
using backend.Models.DTO.Teacher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Teachers
{
    public interface ITeacherRepository
    {
        Task<Teacher> CreateTeacher(Teacher teacher, AddTeacherDTO addTeacherDTO);
        Task<List<Teacher>> GetTeacher();
        Task<Teacher> GetTeacherById(string TeacherId);
        Task<List<Teacher>> GetTeacherBySemester(string Semester);
        Task<Teacher> UpdateTeacher(string TeacherId, Teacher teacher, UpdateTeacherDTO updateTeacherDTO);
        Task<Teacher> DeleteTeacher(string TeacherId, string CollegeId);
        Task<List<CourseTeacherResult>> GetCourseTeacherDataAsync();

    }
}
