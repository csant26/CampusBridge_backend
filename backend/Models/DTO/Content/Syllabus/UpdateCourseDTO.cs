namespace backend.Models.DTO.Content.Syllabus
{
    public class UpdateCourseDTO
    {
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string CourseObjective { get; set; }
        public string FullMarks { get; set; }
        public string PassMarks { get; set; }
        public int CreditHour { get; set; }
        public string LabDescription { get; set; }
        public List<string> Books { get; set; }
        //public string SyllabusId { get; set; }
        public List<UnitDTO> UnitsDTO { get; set; }
    }
}
