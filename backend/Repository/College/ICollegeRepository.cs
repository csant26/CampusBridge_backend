using backend.Models.Domain.Student;

namespace backend.Repository.College
{
    public interface ICollegeRepository
    {
        Task<Student> CreateStudent(Student student);
        Task<List<Student>> GetStudent();
    }
}
