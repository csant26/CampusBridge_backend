using backend.Data;
using backend.Migrations.CampusBridgeDb;
using backend.Models.Domain.Content.Notices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class NoticeRepository : INoticeRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public NoticeRepository(CampusBridgeDbContext campusBridgeDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }
        public async Task<Notice> CreateNotice(Notice notice)
        {
            var user = await userManager.FindByEmailAsync(notice.CreatorId);
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains("ClubHead")) { notice.Creator = "ClubHead"; }
            else if (roles.Contains("College")) { notice.Creator = "College"; }
            else if (roles.Contains("University")) { notice.Creator = "University"; }
            else { return null; }
            notice.DateUpdated = notice.DatePosted;
            await campusBridgeDbContext.Notices.AddAsync(notice);
            await campusBridgeDbContext.SaveChangesAsync();
            return notice;
        }

        public async Task<List<Notice>> GetNotice()
        {
            var notices = await campusBridgeDbContext.Notices.ToListAsync();
            if (notices == null) { return null; }
            return notices;
        }

        public async Task<Notice> GetNoticeById(string NoticeId)
        {
            var notice = await campusBridgeDbContext.Notices.FindAsync(NoticeId);
            if (notice == null) { return null; }
            return notice;
        }
        public async Task<List<Notice>> GetNoticeByCreator(string Creator)
        {
            var notice = await campusBridgeDbContext.Notices.Where(x => x.Creator==Creator)
                .ToListAsync();
            if (notice == null) { return null; }
            return notice;
        }
        public async Task<List<Notice>> GetNoticeByAudience(string Audience)
        {
            var notice = await campusBridgeDbContext.Notices.Where(x => x.DirectedTo.Contains(Audience))
                .ToListAsync();
            if (notice == null) { return null; }
            return notice;
        }
        public async Task<Notice> UpdateNotice(string NoticeId, Notice notice)
        {
            var existingNotice = await GetNoticeById(NoticeId);
            if (existingNotice == null) { return null; }

            if (existingNotice.CreatorId != notice.CreatorId) { return null; }

            existingNotice.Title = notice.Title;
            existingNotice.Description = notice.Description;
            existingNotice.DateUpdated = notice.DateUpdated;
            await campusBridgeDbContext.SaveChangesAsync();
            return existingNotice;
        }

        public async Task<Notice> DeleteNotice(string NoticeId, string CreatorId)
        {
            var existingNotice = await GetNoticeById(NoticeId);
            if (existingNotice == null) { return null; }

            if (existingNotice.CreatorId != CreatorId) { return null; }

            campusBridgeDbContext.Notices.Remove(existingNotice);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingNotice;
        }
    }
}
