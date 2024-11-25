using backend.Models.Domain.Content.Notices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository.Content
{
    public interface INoticeRepository
    {
        Task<Notice> CreateNotice(Notice notice);
        Task<List<Notice>> GetNotice();
        Task<Notice> GetNoticeById(string NoticeId);
        Task<List<Notice>> GetNoticeByCreator(string Creator);
        Task<List<Notice>> GetNoticeByAudience(string Audience);
        Task<Notice> UpdateNotice(string NoticeId, Notice notice);
        Task<Notice> DeleteNotice(string NoticeId, string CreatorId);
    }
}
