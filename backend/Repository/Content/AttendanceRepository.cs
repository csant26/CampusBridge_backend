using backend.Data;
using backend.Models.Domain.Content.Attendances;
using backend.Models.Domain.Students;
using backend.Models.DTO.Content.Attendances;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            attendance.StudentPresence = attendance.StudentPresence ?? new Dictionary<string, bool>();
            foreach (var atnd in addAttendanceDTO.StudentPresence)
            {
                //var existingStudentUser = await userManager.FindByEmailAsync(atnd.Key);
                //if (existingStudentUser == null) { return null; }
                //var roles = await userManager.GetRolesAsync(existingStudentUser);
                //if (!roles.Contains("Student")) { return null; }

                var existingStudent = await campusBridgeDbContext.Students.FirstOrDefaultAsync(x => x.StudentId == atnd.Key);
                if (existingStudent == null) { return null; }

                attendance.StudentPresence.Add(existingStudent.StudentId, atnd.Value);
            }
            attendance.StudentPresenceJson = JsonConvert.SerializeObject(attendance.StudentPresence);

            await campusBridgeDbContext.Attendances.AddAsync(attendance);
            await campusBridgeDbContext.SaveChangesAsync();

            return attendance;
        }
        public async Task<List<Attendance>> GetAttendance()
        {
            var attendance = await campusBridgeDbContext.Attendances.ToListAsync();
            if (attendance != null)
            {
                foreach (var attend in attendance)
                {
                    var studentPresence = JsonConvert.DeserializeObject<Dictionary<string, bool>>(attend.StudentPresenceJson);

                    attend.StudentPresence = studentPresence;
                }
            }
            return attendance;
        }
        public async Task<List<StudentAttendanceData>> GetStudentAttendance(string StudentId)
        {
            var student = await campusBridgeDbContext.Students.FirstOrDefaultAsync(x => (x.Email == StudentId) || (x.StudentId == StudentId));

            List<StudentAttendanceData> studentAttendanceData = new List<StudentAttendanceData>();
            var attendance = await campusBridgeDbContext.Attendances.ToListAsync();
            if (attendance != null)
            {
                foreach (var attend in attendance)
                {
                    StudentAttendanceData std = new StudentAttendanceData();
                    var studentPresence = JsonConvert.DeserializeObject<Dictionary<string, bool>>(attend.StudentPresenceJson);

                    attend.StudentPresence = studentPresence;

                    std.AttendanceDate = attend.AttendanceDate;
                    foreach(var stdpr in studentPresence)
                    {
                        if (stdpr.Key == student.StudentId)
                        {
                            std.isPresent = stdpr.Value;
                        }
                    }
                    studentAttendanceData.Add(std);
                }
            }
            return studentAttendanceData;
        }
    }
}
