namespace backend.Models.Domain.Student
{
    public class Major
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StudentId { get; set; }
    }
}
