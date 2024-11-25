
using backend.Data;
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
            var faqs = await campusBridgeDbContext.FAQs.ToListAsync();

            var bestMatch = faqs
            .Select(faq => new
            {
                faq.Question,
                faq.Answer,
                Score = Fuzz.Ratio(faq.Question, fAQRequestDTO.question) // Fuzzy matching score
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
