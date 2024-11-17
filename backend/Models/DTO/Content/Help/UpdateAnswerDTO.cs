namespace backend.Models.DTO.Content.Help
{
    public class UpdateAnswerDTO
    {
        public string AnswerDetails { get; set; }
        public string AnswerById { get; set; } //Unchangeable but supplied for validation
    }
}
