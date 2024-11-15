using backend.Models.Domain.Students;
using backend.Models.DTO.Student;

namespace backend.Repository.College
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudent(Student student, AddStudentDTO addStudentDTO);
        Task<List<Student>> GetStudent();
        Task<Student> GetStudentById(string id);
        Task<Student> UpdateStudent(string id, Student updatedStudent,UpdateStudentDTO updateStudentDTO);
        Task<Student> DeleteStudent(string id);
    }
}
