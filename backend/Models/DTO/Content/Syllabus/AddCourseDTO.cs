namespace backend.Models.DTO.Content.Syllabus
{
    public class AddCourseDTO
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string CourseObjective { get; set; }
        public bool isElective { get; set; } = false;
        public string FullMarks { get; set; }
        public string PassMarks { get; set; }
        public string CreditHour { get; set; }
        public string LabDescription { get; set; }
        public List<string> Books { get; set; }
        //public string SyllabusId { get; set; }
        public List<UnitDTO> UnitsDTO {  get; set; }
    }
}
