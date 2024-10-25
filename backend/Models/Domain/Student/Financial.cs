namespace backend.Models.Domain.Student
{
    public class Financial
    {
        public int Id { get; set; }
        public bool FeePaid { get; set; }
        public decimal Fee { get; set; }
        public decimal Scholarship { get; set; }=0;
    }
}
