namespace backend.Models.Domain.Content.FAQs
{
    public class FAQ
    {
        public int FAQId {  get; set; }
        public string Question {  get; set; }
        public string Answer {  get; set; }
        public string Category { get; set; }
    }
}
