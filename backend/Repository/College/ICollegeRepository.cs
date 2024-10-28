using backend.Models.Domain.Student;
using backend.Models.DTO.Student;

namespace backend.Repository.College
{
    public interface ICollegeRepository
    {
        Task<Student> CreateStudent(Student student, AddStudentRequestDTO addStudentDTO);
        Task<List<Student>> GetStudent();
        Task<Student> UpdateStudent(string id, Student updatedStudent,UpdateStudentRequestDTO updateStudentDTO);
        Task<Student> DeleteStudent(string id);
    }
}
