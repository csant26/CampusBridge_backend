namespace backend.Models.DTO.Student
{
    public class UpdateStudentRequestDTO
    {
        public string Name { get; set; }
        public ContactDTO ContactDTO { get; set; }
        public FinancialDTO FinancialDTO { get; set; }
        public List<MajorDTO> MajorsDTO { get; set; } = null;
        public List<ClubDTO> ClubsDTO { get; set; } = null;
    }
}
