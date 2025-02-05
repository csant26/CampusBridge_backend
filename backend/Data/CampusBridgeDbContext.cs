using backend.Models.Domain.Colleges;
using backend.Models.Domain.Content.Articles;
using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Attendances;
using backend.Models.Domain.Content.Events;
using backend.Models.Domain.Content.FAQs;
using backend.Models.Domain.Content.Files;
using backend.Models.Domain.Content.Help;
using backend.Models.Domain.Content.Notices;
using backend.Models.Domain.Content.Results;
using backend.Models.Domain.Content.Schedules;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using backend.Models.Domain.Universities;
using backend.Models.DTO.Content.Schedule;
using backend.Models.DTO.Teacher;
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
                .WithMany(s => s.Students);

            modelBuilder.Entity<Student>()
                .HasMany(m => m.Courses)
                .WithMany(s => s.Students);

            ////Article model relationships.
            //modelBuilder.Entity<Article>()
            //    .HasOne(au => au.Author)
            //    .WithMany(ar => ar.Articles)
            //    .HasForeignKey(x => x.AuthorId);

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


            //File-related relationships
            modelBuilder.Entity<FileDomain>()
                .HasKey(key => key.FileId);


            //College-related relationships
            modelBuilder.Entity<College>()
                .HasMany(st => st.Students)
                .WithOne(co => co.College)
                .HasForeignKey(key => key.CollegeId);

            modelBuilder.Entity<College>()
                .HasMany(te => te.Teachers)
                .WithMany(co => co.Colleges);

            //Univeristy-related relationships
            modelBuilder.Entity<University>()
                .HasMany(co=>co.Colleges)
                .WithOne(uni=>uni.University)
                .HasForeignKey(key=>key.UniversityId);



            modelBuilder.Entity<FAQ>().HasData(
            new FAQ { FAQId = 1, Question = "How do I apply for admission?", Answer = "Visit the university website and fill out the online application form.", Category = "Admissions" },
            new FAQ { FAQId = 2, Question = "What documents are required for admission?", Answer = "You'll need transcripts, identification, and recommendation letters.", Category = "Admissions" },
            new FAQ { FAQId = 3, Question = "What are the tuition fees for undergraduate programs?", Answer = "Tuition fees vary by program; visit the fees section on our website.", Category = "Fees" },
            new FAQ { FAQId = 4, Question = "Is financial aid available?", Answer = "Yes, scholarships and financial aid programs are available for eligible students.", Category = "Financial Aid" },
            new FAQ { FAQId = 5, Question = "How can I check my application status?", Answer = "Log in to the student portal to check the status of your application.", Category = "Admissions" },
            new FAQ { FAQId = 6, Question = "Where can I find the academic calendar?", Answer = "The academic calendar is available on the university website under 'Academic Resources'.", Category = "General" },
            new FAQ { FAQId = 7, Question = "What majors does the university offer?", Answer = "The university offers a wide range of programs including Engineering, Business, and Arts.", Category = "Academics" },
            new FAQ { FAQId = 8, Question = "Can I change my major after admission?", Answer = "Yes, but you need to complete a major change request form through the academic office.", Category = "Academics" },
            new FAQ { FAQId = 9, Question = "What is the grading system?", Answer = "The university follows a GPA-based grading system ranging from A to F.", Category = "Academics" },
            new FAQ { FAQId = 10, Question = "How do I register for courses?", Answer = "You can register through the student portal during the enrollment period.", Category = "Registration" },
            new FAQ { FAQId = 11, Question = "Can I take online courses?", Answer = "Yes, several programs offer online or hybrid learning options.", Category = "Academics" },
            new FAQ { FAQId = 12, Question = "Is hostel accommodation available?", Answer = "Yes, the university provides on-campus and off-campus housing options.", Category = "Facilities" },
            new FAQ { FAQId = 13, Question = "How do I apply for a student visa?", Answer = "After admission, the university provides required documents for visa applications.", Category = "International Students" },
            new FAQ { FAQId = 14, Question = "What sports facilities are available?", Answer = "The university offers a gym, swimming pool, and various sports fields.", Category = "Facilities" },
            new FAQ { FAQId = 15, Question = "Can I work while studying?", Answer = "Yes, students can work up to 20 hours per week on campus.", Category = "General" },
            new FAQ { FAQId = 16, Question = "How do I get a student ID card?", Answer = "Student ID cards are issued by the administration office during orientation.", Category = "General" },
            new FAQ { FAQId = 17, Question = "How do I reset my student portal password?", Answer = "Use the 'Forgot Password' option on the login page.", Category = "Technical Support" },
            new FAQ { FAQId = 18, Question = "When will my exam results be released?", Answer = "Exam results are typically published within three weeks after exams.", Category = "Academics" },
            new FAQ { FAQId = 19, Question = "Can I get a refund for my tuition fees?", Answer = "Tuition refunds are available under specific conditions; refer to the refund policy.", Category = "Fees" },
            new FAQ { FAQId = 20, Question = "Are internships mandatory?", Answer = "Some programs require internships as part of the curriculum.", Category = "Academics" },
            new FAQ { FAQId = 21, Question = "What extracurricular activities are available?", Answer = "There are many clubs and organizations students can join.", Category = "Student Life" },
            new FAQ { FAQId = 22, Question = "How do I submit my assignments?", Answer = "Assignments can be submitted via the student portal or email, as instructed.", Category = "Academics" },
            new FAQ { FAQId = 23, Question = "What should I do if I have a complaint?", Answer = "You can file a formal complaint through the student services office.", Category = "General" },
            new FAQ { FAQId = 24, Question = "Does the university offer exchange programs?", Answer = "Yes, the university partners with several institutions worldwide.", Category = "International Students" },
            new FAQ { FAQId = 25, Question = "How can I contact my professors?", Answer = "You can reach them via email or during office hours.", Category = "Academics" },
            new FAQ { FAQId = 26, Question = "Can I defer my admission?", Answer = "Yes, you can apply for admission deferral for up to one year.", Category = "Admissions" },
            new FAQ { FAQId = 27, Question = "How can I request my transcripts?", Answer = "Transcripts can be requested via the registrar's office.", Category = "Academics" },
            new FAQ { FAQId = 28, Question = "Is medical assistance available on campus?", Answer = "Yes, there is a health center for students.", Category = "Facilities" },
            new FAQ { FAQId = 29, Question = "How do I join a research project?", Answer = "Reach out to faculty members leading research in your field of interest.", Category = "Academics" },
            new FAQ { FAQId = 30, Question = "Is parking available on campus?", Answer = "Yes, students can apply for parking permits.", Category = "Facilities" },
            new FAQ { FAQId = 31, Question = "How do I get my degree certificate?", Answer = "Certificates are issued after graduation from the registrar’s office.", Category = "Academics" },
            new FAQ { FAQId = 32, Question = "Are there job placement services?", Answer = "Yes, the university has a career center to assist students.", Category = "Student Life" },
            new FAQ { FAQId = 33, Question = "Can I use my student ID for public transport?", Answer = "Yes, student ID cards may offer discounts on public transport.", Category = "General" },
            new FAQ { FAQId = 34, Question = "Is there a cafeteria on campus?", Answer = "Yes, the university provides multiple dining options.", Category = "Facilities" },
            new FAQ { FAQId = 35, Question = "What if I miss an exam?", Answer = "You need to apply for a make-up exam with valid reasons.", Category = "Academics" },
            new FAQ { FAQId = 36, Question = "How do I find academic advisors?", Answer = "Advisors are assigned based on your major; details are available in the portal.", Category = "Academics" },
            new FAQ { FAQId = 37, Question = "Can I bring my pet to campus?", Answer = "Only service animals are allowed in dorms and classes.", Category = "General" },
            new FAQ { FAQId = 38, Question = "Are there special services for disabled students?", Answer = "Yes, the university offers accessibility support services.", Category = "Facilities" },
            new FAQ { FAQId = 39, Question = "Where can I report lost items?", Answer = "Lost items should be reported to campus security.", Category = "General" },
            new FAQ { FAQId = 40, Question = "How do I participate in student elections?", Answer = "Elections are organized by the student council; check the portal for dates.", Category = "Student Life" }
        );

            modelBuilder.Entity<CourseTeacherResult>().HasNoKey();
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

        //College-related Tables
        public DbSet<College> Colleges { get; set; }
        
        //University-related Tables
        public DbSet<University> Universities {  get; set; }

        //Result-related Tables
        public DbSet<Result> Results { get; set; }

        //FAQ-related Tables
        public DbSet<FAQ> FAQs {  get; set; }
        
        //Schedule-related Tables
        public DbSet<Schedule> Schedules {  get; set; }

        //Attendance-related Tables
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<TeacherSchedule> TeacherSchedules { get; set; }

    }
}
