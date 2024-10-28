namespace backend.Models.Domain.Student
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        //public string SymbolNo { get; set; }
        //public string RegistrationNo {  get; set; }
        //Foreign Keys
        public string AcademicId { get; set; }
        public string FinancialId { get; set; }
        //Navigation Properties
        public Academic Academic { get; set; } //One-to-one
        public Financial Financial { get; set; } //One-to-one
        public List<Major> Majors { get; set; } //One-to-many
        public List<Club>? Clubs { get; set; } //One-to-many
    }
}
