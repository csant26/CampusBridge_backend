using backend.Models.Domain.Content.Notices;

namespace backend.Repository.Content
{
    public interface INoticeRepository
    {
        Task<Notice> CreateNotice(string creatorId, Notice notice);
        Task<List<Notice>> GetNotice();
        Task<Notice> GetNoticeById(string id);
        Task<Notice> UpdateNotice(string NoticeId, string creatorId, Notice notice);
        Task<Notice> DeleteNotice(string creatorId, string NoticeId);
    }
}
