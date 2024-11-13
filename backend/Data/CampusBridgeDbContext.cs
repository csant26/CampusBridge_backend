using backend.Models.Domain.Content.Article;
using backend.Models.Domain.Content.Syllabus;
using backend.Models.Domain.Student;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CampusBridgeDbContext : DbContext
    {
        public CampusBridgeDbContext(DbContextOptions<CampusBridgeDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Student model relationships.
            modelBuilder.Entity<Student>()
                .HasOne(a => a.Academic)
                .WithMany(s => s.Students)
                .HasForeignKey(fk => fk.AcademicId);

            modelBuilder.Entity<Student>()
                .HasOne(f => f.Financial)
                .WithMany(s => s.Students)
                .HasForeignKey(fk => fk.FinancialId);

            modelBuilder.Entity<Student>()
                .HasMany(c => c.Clubs)
                .WithMany(s => s.Students)
                ;

            modelBuilder.Entity<Student>()
                .HasMany(m => m.Majors)
                .WithMany(s => s.Students);

            //Article model relationships.
            modelBuilder.Entity<Article>()
                .HasOne(au => au.Author)
                .WithMany(ar => ar.Articles)
                .HasForeignKey(x => x.AuthorId);

            //Syllabus model relationships
            modelBuilder.Entity<Syllabus>()
                .HasMany(co => co.Courses)
                .WithOne(sy => sy.Syllabus)
                .HasForeignKey(key => key.SyllabusId);

            modelBuilder.Entity<Course>()
                .HasMany(un => un.Units)
                .WithOne(co => co.Course)
                .HasForeignKey(key => key.CourseId);


        }

        //Student-related Tables.
        public DbSet<Student> Students { get; set; }
        public DbSet<Academic> Academics {  get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Financial> Financials { get; set; }
        public DbSet<Major> Majors { get; set; }
        
        //Article-related Tables.
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }

        //Syllabus-related Tables
        public DbSet<Syllabus> Syllabus { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Unit> Unit { get; set; }
    }
}
