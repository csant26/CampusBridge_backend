namespace backend.Models.DTO.Student
{
    public class AddStudentDTO
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public bool? isClubHead { get; set; } = false;
        public bool? isAuthor { get; set; } = false;
        public string FinancialId {  get; set; }
        public string AcademicId {  get; set; }
        public List<string>? ElectiveIds { get; set; } = null;
        public List<string>? ClubIds { get; set; } = null;
    }
}
