namespace backend.Models.DTO.Student
{
    public class AddStudentRequestDTO
    {
        public string Name { get; set; }
        public ContactDTO ContactDTO { get; set; }
        public FinancialDTO FinancialDTO { get; set; }
        public AcademicDTO AcademicDTO { get; set; }
        public List<MajorDTO> MajorsDTO { get; set; } = null;
        public List<ClubDTO> ClubsDTO { get; set; } = null;
    }
}
