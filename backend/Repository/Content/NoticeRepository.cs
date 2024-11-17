using backend.Data;
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
        public async Task<Notice> CreateNotice(string creatorId, Notice notice)
        {
            var user = await userManager.FindByEmailAsync(creatorId);
            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (role == null) { return null; }
            else if (role == "College" || role =="University" ) { notice.Creator = role; }
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

        public async Task<Notice> GetNoticeById(string id)
        {
            var notice = await campusBridgeDbContext.Notices.FindAsync(id);
            if (notice == null) { return null; }
            return notice;
        }

        public async Task<List<Notice>> GetNoticeByRole(string RoleName)
        {
            var notice = await campusBridgeDbContext.Notices.Where(x => x.Creator == RoleName).ToListAsync();
            if (notice == null) { return null; }
            return notice;
        }
        public async Task<Notice> UpdateNotice(string NoticeId, string creatorId, Notice notice)
        {
            var existingNotice = await GetNoticeById(NoticeId);
            if (existingNotice == null) { return null; }

            var user = await userManager.FindByEmailAsync(creatorId);
            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (existingNotice.Creator != role) { return null; }
            existingNotice.Title = notice.Title;
            existingNotice.Description = notice.Description;
            existingNotice.DateUpdated = notice.DateUpdated;
            await campusBridgeDbContext.SaveChangesAsync();
            return existingNotice;
        }

        public async Task<Notice> DeleteNotice(string NoticeId, string creatorId)
        {
            var existingNotice = await GetNoticeById(NoticeId);
            if (existingNotice == null) { return null; }

            var user = await userManager.FindByEmailAsync(creatorId);
            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (existingNotice.Creator != role) { return null; }
            campusBridgeDbContext.Notices.Remove(existingNotice);
            await campusBridgeDbContext.SaveChangesAsync();
            return existingNotice;
        }
    }
}
