namespace backend.Models.Domain.Student
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContactId { get; set; }
        public int AcademicId { get; set; }
        public int FinancialId { get; set; }

        //Navigation Properties
        public Contact Contact { get; set; }
        public Academic Academic { get; set; }
        public Financial Financial { get; set; }
        public List<Major> Majors { get; set; }
        public List<Club>? Clubs { get; set; }
    }
}
