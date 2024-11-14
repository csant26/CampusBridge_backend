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
    [Migration("20241114125340_image handling")]
    partial class imagehandling
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
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

            modelBuilder.Entity("MajorStudent", b =>
                {
                    b.Property<string>("MajorsMajorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentsStudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MajorsMajorId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("MajorStudent");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Article.Article", b =>
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

            modelBuilder.Entity("backend.Models.Domain.Content.Article.Author", b =>
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

            modelBuilder.Entity("backend.Models.Domain.Content.Image.Image", b =>
                {
                    b.Property<string>("ImageId")
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

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Books")
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

                    b.HasKey("CourseId");

                    b.HasIndex("SyllabusId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Syllabus", b =>
                {
                    b.Property<string>("SyllabusId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SyllabusId");

                    b.ToTable("Syllabus");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Unit", b =>
                {
                    b.Property<string>("UnitId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CompletionHours")
                        .HasColumnType("int");

                    b.Property<string>("CourseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubUnits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.HasIndex("CourseId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Academic", b =>
                {
                    b.Property<string>("AcademicId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Batch")
                        .HasColumnType("int");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademicId");

                    b.ToTable("Academics");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Club", b =>
                {
                    b.Property<string>("ClubId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClubId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Financial", b =>
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

            modelBuilder.Entity("backend.Models.Domain.Student.Major", b =>
                {
                    b.Property<string>("MajorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajorId");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Student", b =>
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

            modelBuilder.Entity("ClubStudent", b =>
                {
                    b.HasOne("backend.Models.Domain.Student.Club", null)
                        .WithMany()
                        .HasForeignKey("ClubsClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Student.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MajorStudent", b =>
                {
                    b.HasOne("backend.Models.Domain.Student.Major", null)
                        .WithMany()
                        .HasForeignKey("MajorsMajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Student.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Article.Article", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Article.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Course", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabus.Syllabus", "Syllabus")
                        .WithMany("Courses")
                        .HasForeignKey("SyllabusId");

                    b.Navigation("Syllabus");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Unit", b =>
                {
                    b.HasOne("backend.Models.Domain.Content.Syllabus.Course", "Course")
                        .WithMany("Units")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Student", b =>
                {
                    b.HasOne("backend.Models.Domain.Student.Academic", "Academic")
                        .WithMany("Students")
                        .HasForeignKey("AcademicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Models.Domain.Student.Financial", "Financial")
                        .WithMany("Students")
                        .HasForeignKey("FinancialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academic");

                    b.Navigation("Financial");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Article.Author", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Course", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("backend.Models.Domain.Content.Syllabus.Syllabus", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Academic", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("backend.Models.Domain.Student.Financial", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
