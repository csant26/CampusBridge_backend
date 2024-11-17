namespace backend.Models.DTO.College
{
    public class UpdateCollegeDTO
    {
        public int CollegeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
