namespace backend.Models.DTO.Student
{
    public class FinancialDTO
    {
        public int Id { get; set; }
        public bool FeePaid { get; set; }
        public decimal Fee { get; set; }
        public decimal Scholarship { get; set; } = 0;
    }
}
