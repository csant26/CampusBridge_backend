using backend.Models.Domain.Student;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CampusBridgeDbContext : DbContext
    {
        public CampusBridgeDbContext(DbContextOptions<CampusBridgeDbContext> options):base(options) { }

        //Student-related Tables.
        public DbSet<Student> Students { get; set; }
        public DbSet<Academic> Academics {  get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Financial> Financials { get; set; }
        public DbSet<Major> Majors { get; set; }
    }
}
