namespace backend.Models.DTO.Student
{
    public class UpdateStudentDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Semseter { get; set; }
        public bool? isClubHead { get; set; } = false;
        public List<string>? ElectiveIds { get; set; } = null;
        public string FinancialId { get; set; }
        public List<string>? ClubIds { get; set; }
    }
}
