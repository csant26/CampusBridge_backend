namespace backend.Models.DTO.Student
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool isPresent { get; set; }
    }
}
