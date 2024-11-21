namespace backend.Models.DTO.Teacher
{
    public class UpdateTeacherDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<string> CourseIds { get; set; }
        public string CreatorId { get; set; } //Creator: Teacher or College
    }
}
