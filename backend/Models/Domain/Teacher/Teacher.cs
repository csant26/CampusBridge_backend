using System.ComponentModel.DataAnnotations;

namespace backend.Models.Domain.TeacherDomain
{
    public class Teacher
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        //subject, positions, working hours
    }
}
