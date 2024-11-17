namespace backend.Models.DTO.Student
{
    public class AddClubDTO
    {
        public string ClubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClubHeadId { get; set; } //Supplied for validation
        public List<string> StudentId { get; set; }
    }
}
