﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    [DbContext(typeof(CampusBridgeDbContext))]
    [Migration("20241116113054_notices")]
    partial class notices
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClubStudent", b =>
                {
                    b.Property<string>("ClubsClubId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentsStudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClubsClubId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("ClubStudent");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<string>("CoursesCourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentsStudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CoursesCourseId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("CourseTeacher", b =>
                {
                    b.Property<string>("CoursesCourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeachersTeacherId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CoursesCourseId", "TeachersTeacherId");

                    b.HasIndex("TeachersTeacherId");

                    b.ToTable("CourseTeacher");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Articles.Article", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tagline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Articles.Author", b =>
                {
                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CampusId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Assignments.Assignment", b =>
                {
                    b.Property<string>("AssignmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AssignmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Assignments.Submission", b =>
                {
                    b.Property<string>("SubmissionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SubmissionId");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Files.FileDomain", b =>
                {
                    b.Property<string>("FileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.HasKey("FileId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Notices.Notice", b =>
                {
                    b.Property<string>("NoticeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoticeId");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.PrimitiveCollection<string>("Books")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseObjective")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreditHour")
                        .HasColumnType("int");

                    b.Property<string>("FullMarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LabDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassMarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SyllabusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isElective")
                        .HasColumnType("bit");

                    b.HasKey("CourseId");

                    b.HasIndex("SyllabusId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Syllabus", b =>
                {
                    b.Property<string>("SyllabusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AllowedElectiveNo")
                        .HasColumnType("int");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SyllabusId");

                    b.ToTable("Syllabus");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Unit", b =>
                {
                    b.Property<string>("UnitId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CompletionHours")
                        .HasColumnType("int");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.PrimitiveCollection<string>("SubUnits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.HasIndex("CourseId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Academic", b =>
                {
                    b.Property<string>("AcademicId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Batch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademicId");

                    b.ToTable("Academics");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Club", b =>
                {
                    b.Property<string>("ClubId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClubId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Financial", b =>
                {
                    b.Property<string>("FinancialId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("FeePaid")
                        .HasColumnType("bit");

                    b.Property<decimal>("Scholarship")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FinancialId");

                    b.ToTable("Financials");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AcademicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinancialId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("AcademicId");

                    b.HasIndex("FinancialId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("backend.Models.Domain.Teachers.Teacher", b =>
                {
                    b.Property<string>("TeacherId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ClubStudent", b =>
                {
                    b.HasOne("backend.Models.Domain.Students.Club", null)
                        .WithMany()
                        .HasForeignKey("ClubsClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Students.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabi.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Students.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseTeacher", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabi.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Teachers.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Articles.Article", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Articles.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Assignments.Assignment", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabi.Course", "Course")
                        .WithMany("Assignments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Teachers.Teacher", "Teacher")
                        .WithMany("Assignments")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Assignments.Submission", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Assignments.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Students.Student", "Student")
                        .WithMany("Submissions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Course", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabi.Syllabus", "Syllabus")
                        .WithMany("Courses")
                        .HasForeignKey("SyllabusId");

                    b.Navigation("Syllabus");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Unit", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabi.Course", "Course")
                        .WithMany("Units")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Student", b =>
                {
                    b.HasOne("backend.Models.Domain.Students.Academic", "Academic")
                        .WithMany("Students")
                        .HasForeignKey("AcademicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Students.Financial", "Financial")
                        .WithMany("Students")
                        .HasForeignKey("FinancialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academic");

                    b.Navigation("Financial");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Articles.Author", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Assignments.Assignment", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Course", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Units");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabi.Syllabus", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Academic", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Financial", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("backend.Models.Domain.Students.Student", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("backend.Models.Domain.Teachers.Teacher", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
