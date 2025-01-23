using backend.Models.DTO.Content.FAQ;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface IFAQRepository
    {
        Task<FAQResponseDTO> GetAnswer(FAQRequestDTO fAQRequestDTO );
        Task<string> PredictAnswerAsync(string question);
    }
}
