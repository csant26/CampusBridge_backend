using backend.Models.Domain.Content.Articles;
using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Events;
using backend.Models.Domain.Content.Files;
using backend.Models.Domain.Content.Help;
using backend.Models.Domain.Content.Notices;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
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
                .HasMany(m => m.Courses)
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

            //Assignment related relationships
            modelBuilder.Entity<Teacher>()
                .HasMany(co => co.Courses)
                .WithMany(te => te.Teachers);

            modelBuilder.Entity<Teacher>()
                .HasMany(a=>a.Assignments)
                .WithOne(te => te.Teacher)
                .HasForeignKey(key => key.TeacherId);

            modelBuilder.Entity<Course>()
                .HasMany(a => a.Assignments)
                .WithOne(c => c.Course)
                .HasForeignKey(key => key.CourseId);

            modelBuilder.Entity<Assignment>()
                .HasMany(s=>s.Submissions)
                .WithOne(a => a.Assignment)
                .HasForeignKey(key => key.AssignmentId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany(i => i.Submissions)
                .HasForeignKey(key => key.StudentId);

            modelBuilder.Entity<FileDomain>()
                .HasKey(key => key.FileId);

        }

        //Student-related Tables.
        public DbSet<Student> Students { get; set; }
        public DbSet<Academic> Academics {  get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Financial> Financials { get; set; }
        
        //Article-related Tables.
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }

        //Syllabus-related Tables
        public DbSet<Syllabus> Syllabus { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Unit> Unit { get; set; }

        //File-related Tables
        public DbSet<FileDomain> Files { get; set; }

        //Assignment-related Tables
        public DbSet<Assignment> Assignments {  get; set; }
        public DbSet<Submission> Submissions { get; set; }

        //Notice-related Tables
        public DbSet<Notice> Notices { get; set; }

        //Help-related Tables
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        //Event-related Tables
        public DbSet<Event> Events {  get; set; }

        //Teacher-related Tables
        public DbSet<Teacher> Teachers { get; set; }


    }
}
