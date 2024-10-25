using backend.Models.Domain.Student;

namespace backend.Models.DTO.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContactDTO ContactDTO { get; set; }
        public AcademicDTO AcademicDTO { get; set; }
        public FinancialDTO FinancialDTO { get; set; }
        public List<MajorDTO> MajorsDTO { get; set; } = null;
        public List<ClubDTO> ClubsDTO { get; set; } = null;
    }
}
