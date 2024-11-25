using backend.Models.Domain.Students;
using backend.Models.DTO.Student;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Students
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudent(Student student, AddStudentDTO addStudentDTO);
        Task<List<Student>> GetStudent();
        Task<Student> GetStudentById(string id);
        Task<Student> UpdateStudent(string id, Student updatedStudent,UpdateStudentDTO updateStudentDTO);
        Task<Student> DeleteStudent(string StudentId, string CollegeId);
    }
}
