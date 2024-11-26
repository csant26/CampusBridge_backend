
using backend.Data;
using backend.Models.Domain.Content.FAQs;
using backend.Models.DTO.Content.FAQ;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public class FAQRepository : IFAQRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public FAQRepository(CampusBridgeDbContext campusBridgeDbContext)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
        }
        public async Task<FAQResponseDTO> GetAnswer(FAQRequestDTO fAQRequestDTO)
        {
            if(fAQRequestDTO.Question==null && fAQRequestDTO.Category == null) { return null; }
            var faqs = new List<FAQ>();
            if (fAQRequestDTO.Question != null) {
                faqs = await campusBridgeDbContext.FAQs.ToListAsync();
            }
            if (fAQRequestDTO.Category != null)
            {
                faqs = await campusBridgeDbContext.FAQs
                    .Where(x=>x.Category==fAQRequestDTO.Category)
                    .ToListAsync();
            }
            var bestMatch = faqs
            .Select(faq => new
            {
                faq.Question,
                faq.Answer,
                Score = Fuzz.Ratio(faq.Question, fAQRequestDTO.Question) // Fuzzy matching score
            })
            .OrderByDescending(x => x.Score)
            .FirstOrDefault();

            if (bestMatch != null) // Threshold for a good match
            {
                return new FAQResponseDTO { answer = bestMatch.Answer, score=bestMatch.Score };
            }
            else
            {
                return new FAQResponseDTO { answer = "No relevant answer.", score = 0 };
            }
        }
    }
}
