using backend.Models.DTO.Student;
using backend.Models.DTO.Teacher;

namespace backend.Models.DTO.College
{
    public class AddCollegeDTO
    {
        public string CollegeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string UniversityId {  get; set; } //Supplied for validation
    }
}
