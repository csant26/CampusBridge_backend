using Microsoft.ML.Data;

namespace backend.Models.Domain.Content.FAQs
{
    public class FAQData
    {
        [LoadColumn(0)]
        public string Question { get;set; }
        [LoadColumn(1)]
        public string Answer { get; set; }
    }
}
