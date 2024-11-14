using backend.Models.Domain.Content.Syllabus;
using backend.Models.DTO.Content.Syllabus;

namespace backend.Repository.Content
{
    public interface ISyllabusRepository
    {
        Task<Course> CreateCourse(Course course);
        Task<List<Course>> GetCourse();
        Task<Course> GetCourseById(string CourseId);
        Task<Course> UpdateCourse(string CourseId, Course course);
        Task<Course> DeleteCourse(string CourseId);
        
        Task<Syllabus> CreateSyllabus(Syllabus syllabus,AddSyllabusDTO addSyllabusDTO);
        Task<List<Syllabus>> GetSyllabus();
        Task<Syllabus> GetSyllabusById(string SyllabusId);
        Task<Syllabus> UpdateSyllabus(string SyllabusId, Syllabus syllabus, UpdateSyllabusDTO updateSyllabusDTO);
        Task<Syllabus> DeleteSyllabus(string SyllabusId);
    }
}
