namespace backend.Models.DTO.Student
{
    public class AddStudentDTO
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string FinancialId {  get; set; }
        public string AcademicId {  get; set; }
        public List<string>? ElectiveIds { get; set; } = null;
        public List<string>? ClubIds { get; set; } = null;
    }
}
