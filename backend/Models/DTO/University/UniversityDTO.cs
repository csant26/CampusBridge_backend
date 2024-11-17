using backend.Models.DTO.College;

namespace backend.Models.DTO.University
{
    public class UniversityDTO
    {
        public string UniversityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<CollegeDTO> CollegeDTO { get; set; }
    }
}
