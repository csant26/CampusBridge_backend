using backend.Data;
using backend.Models.Domain.Content.Attendances;
using backend.Models.DTO.Content.Attendances;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Content
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly CampusBridgeDbContext campusBridgeDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public AttendanceRepository(CampusBridgeDbContext campusBridgeDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.campusBridgeDbContext = campusBridgeDbContext;
            this.userManager = userManager;
        }

        public async Task<Attendance> CreateAttendance(Attendance attendance,
            AddAttendanceDTO addAttendanceDTO)
        {
            foreach(var atnd in addAttendanceDTO.StudentPresence)
            {
                var existingStudentUser = await userManager.FindByEmailAsync(atnd.Key);
                if (existingStudentUser == null) { return null; }
                var roles = await userManager.GetRolesAsync(existingStudentUser);
                if (!roles.Contains("Student")) { return null; }

                var existingStudent = await campusBridgeDbContext.Students.FindAsync(atnd.Key);
                if (existingStudent == null) { return null; }

                attendance.StudentPresence.Add(existingStudent,atnd.Value);
            }

            await campusBridgeDbContext.Attendances.AddAsync(attendance);
            await campusBridgeDbContext.SaveChangesAsync();

            return attendance;
        }
        public async Task<List<Attendance>> GetAttendance()
        {
            return await campusBridgeDbContext.Attendances.ToListAsync();
        }
    }
}
