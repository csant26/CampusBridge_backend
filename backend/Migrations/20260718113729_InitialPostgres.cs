using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "academics",
                columns: table => new
                {
                    academic_id = table.Column<string>(type: "text", nullable: false),
                    batch = table.Column<string>(type: "text", nullable: false),
                    semester = table.Column<string>(type: "text", nullable: false),
                    faculty = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_academics", x => x.academic_id);
                });

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    attendance_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    student_presence_json = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    author_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    author_type = table.Column<string>(type: "text", nullable: false),
                    campus_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "clubs",
                columns: table => new
                {
                    club_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    club_head_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clubs", x => x.club_id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    event_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    event_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_posted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    directed_to = table.Column<List<string>>(type: "text[]", nullable: false),
                    creator_id = table.Column<string>(type: "text", nullable: false),
                    creator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.event_id);
                });

            migrationBuilder.CreateTable(
                name: "faqs",
                columns: table => new
                {
                    faq_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question = table.Column<string>(type: "text", nullable: false),
                    answer = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faqs", x => x.faq_id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    file_id = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    file_description = table.Column<string>(type: "text", nullable: true),
                    file_extension = table.Column<string>(type: "text", nullable: false),
                    file_size_in_bytes = table.Column<long>(type: "bigint", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.file_id);
                });

            migrationBuilder.CreateTable(
                name: "financials",
                columns: table => new
                {
                    financial_id = table.Column<string>(type: "text", nullable: false),
                    fee_paid = table.Column<bool>(type: "boolean", nullable: false),
                    fee = table.Column<decimal>(type: "numeric", nullable: false),
                    scholarship = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financials", x => x.financial_id);
                });

            migrationBuilder.CreateTable(
                name: "notices",
                columns: table => new
                {
                    notice_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    directed_to = table.Column<List<string>>(type: "text[]", nullable: false),
                    creator = table.Column<string>(type: "text", nullable: false),
                    creator_id = table.Column<string>(type: "text", nullable: false),
                    date_posted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notices", x => x.notice_id);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    directed_to = table.Column<List<string>>(type: "text[]", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "syllabus",
                columns: table => new
                {
                    syllabus_id = table.Column<string>(type: "text", nullable: false),
                    semester = table.Column<string>(type: "text", nullable: false),
                    allowed_elective_no = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_syllabus", x => x.syllabus_id);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    teacher_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teachers", x => x.teacher_id);
                });

            migrationBuilder.CreateTable(
                name: "universities",
                columns: table => new
                {
                    university_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    creator_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_universities", x => x.university_id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    article_id = table.Column<string>(type: "text", nullable: false),
                    headline = table.Column<string>(type: "text", nullable: false),
                    tagline = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    creator_id = table.Column<string>(type: "text", nullable: false),
                    date_posted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    author_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articles", x => x.article_id);
                    table.ForeignKey(
                        name: "fk_articles_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "author_id");
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    course_id = table.Column<string>(type: "text", nullable: false),
                    course_title = table.Column<string>(type: "text", nullable: false),
                    course_description = table.Column<string>(type: "text", nullable: false),
                    course_objective = table.Column<string>(type: "text", nullable: false),
                    is_elective = table.Column<bool>(type: "boolean", nullable: false),
                    full_marks = table.Column<string>(type: "text", nullable: false),
                    pass_marks = table.Column<string>(type: "text", nullable: false),
                    credit_hour = table.Column<int>(type: "integer", nullable: false),
                    lab_description = table.Column<string>(type: "text", nullable: false),
                    books = table.Column<List<string>>(type: "text[]", nullable: false),
                    syllabus_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course", x => x.course_id);
                    table.ForeignKey(
                        name: "fk_course_syllabus_syllabus_id",
                        column: x => x.syllabus_id,
                        principalTable: "syllabus",
                        principalColumn: "syllabus_id");
                });

            migrationBuilder.CreateTable(
                name: "colleges",
                columns: table => new
                {
                    college_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    university_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colleges", x => x.college_id);
                    table.ForeignKey(
                        name: "fk_colleges_universities_university_id",
                        column: x => x.university_id,
                        principalTable: "universities",
                        principalColumn: "university_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignments",
                columns: table => new
                {
                    assignment_id = table.Column<string>(type: "text", nullable: false),
                    question = table.Column<string>(type: "text", nullable: false),
                    course_id = table.Column<string>(type: "text", nullable: false),
                    assigned_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    submission_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: true),
                    teacher_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignments", x => x.assignment_id);
                    table.ForeignKey(
                        name: "fk_assignments_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assignments_teachers_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_teacher",
                columns: table => new
                {
                    courses_course_id = table.Column<string>(type: "text", nullable: false),
                    teachers_teacher_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_teacher", x => new { x.courses_course_id, x.teachers_teacher_id });
                    table.ForeignKey(
                        name: "fk_course_teacher_course_courses_course_id",
                        column: x => x.courses_course_id,
                        principalTable: "course",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_course_teacher_teachers_teachers_teacher_id",
                        column: x => x.teachers_teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    unit_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    completion_hours = table.Column<int>(type: "integer", nullable: false),
                    sub_units = table.Column<List<string>>(type: "text[]", nullable: false),
                    course_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit", x => x.unit_id);
                    table.ForeignKey(
                        name: "fk_unit_course_course_id",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "college_teacher",
                columns: table => new
                {
                    colleges_college_id = table.Column<string>(type: "text", nullable: false),
                    teachers_teacher_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_college_teacher", x => new { x.colleges_college_id, x.teachers_teacher_id });
                    table.ForeignKey(
                        name: "fk_college_teacher_colleges_colleges_college_id",
                        column: x => x.colleges_college_id,
                        principalTable: "colleges",
                        principalColumn: "college_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_college_teacher_teachers_teachers_teacher_id",
                        column: x => x.teachers_teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    student_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    is_club_head = table.Column<bool>(type: "boolean", nullable: true),
                    is_author = table.Column<bool>(type: "boolean", nullable: true),
                    academic_id = table.Column<string>(type: "text", nullable: false),
                    financial_id = table.Column<string>(type: "text", nullable: false),
                    college_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.student_id);
                    table.ForeignKey(
                        name: "fk_students_academics_academic_id",
                        column: x => x.academic_id,
                        principalTable: "academics",
                        principalColumn: "academic_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_students_colleges_college_id",
                        column: x => x.college_id,
                        principalTable: "colleges",
                        principalColumn: "college_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_students_financials_financial_id",
                        column: x => x.financial_id,
                        principalTable: "financials",
                        principalColumn: "financial_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "club_student",
                columns: table => new
                {
                    clubs_club_id = table.Column<string>(type: "text", nullable: false),
                    students_student_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_club_student", x => new { x.clubs_club_id, x.students_student_id });
                    table.ForeignKey(
                        name: "fk_club_student_clubs_clubs_club_id",
                        column: x => x.clubs_club_id,
                        principalTable: "clubs",
                        principalColumn: "club_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_club_student_students_students_student_id",
                        column: x => x.students_student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_student",
                columns: table => new
                {
                    courses_course_id = table.Column<string>(type: "text", nullable: false),
                    students_student_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_student", x => new { x.courses_course_id, x.students_student_id });
                    table.ForeignKey(
                        name: "fk_course_student_course_courses_course_id",
                        column: x => x.courses_course_id,
                        principalTable: "course",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_course_student_students_students_student_id",
                        column: x => x.students_student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    question_id = table.Column<string>(type: "text", nullable: false),
                    question_details = table.Column<string>(type: "text", nullable: false),
                    directed_to = table.Column<List<string>>(type: "text[]", nullable: false),
                    student_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_questions", x => x.question_id);
                    table.ForeignKey(
                        name: "fk_questions_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    result_id = table.Column<string>(type: "text", nullable: false),
                    examination_type = table.Column<string>(type: "text", nullable: false),
                    semester = table.Column<string>(type: "text", nullable: false),
                    percentage = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    student_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_results", x => x.result_id);
                    table.ForeignKey(
                        name: "fk_results_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "submissions",
                columns: table => new
                {
                    submission_id = table.Column<string>(type: "text", nullable: false),
                    answer = table.Column<string>(type: "text", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: true),
                    score = table.Column<string>(type: "text", nullable: true),
                    student_id = table.Column<string>(type: "text", nullable: false),
                    assignment_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_submissions", x => x.submission_id);
                    table.ForeignKey(
                        name: "fk_submissions_assignments_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "assignments",
                        principalColumn: "assignment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_submissions_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    answer_id = table.Column<string>(type: "text", nullable: false),
                    answer_details = table.Column<string>(type: "text", nullable: false),
                    answer_by_id = table.Column<string>(type: "text", nullable: false),
                    question_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_answers", x => x.answer_id);
                    table.ForeignKey(
                        name: "fk_answers_questions_question_id",
                        column: x => x.question_id,
                        principalTable: "questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "faqs",
                columns: new[] { "faq_id", "answer", "category", "question" },
                values: new object[,]
                {
                    { 1, "Visit the university website and fill out the online application form.", "Admissions", "How do I apply for admission?" },
                    { 2, "You'll need transcripts, identification, and recommendation letters.", "Admissions", "What documents are required for admission?" },
                    { 3, "Tuition fees vary by program; visit the fees section on our website.", "Fees", "What are the tuition fees for undergraduate programs?" },
                    { 4, "Yes, scholarships and financial aid programs are available for eligible students.", "Financial Aid", "Is financial aid available?" },
                    { 5, "Log in to the student portal to check the status of your application.", "Admissions", "How can I check my application status?" },
                    { 6, "The academic calendar is available on the university website under 'Academic Resources'.", "General", "Where can I find the academic calendar?" },
                    { 7, "The university offers a wide range of programs including Engineering, Business, and Arts.", "Academics", "What majors does the university offer?" },
                    { 8, "Yes, but you need to complete a major change request form through the academic office.", "Academics", "Can I change my major after admission?" },
                    { 9, "The university follows a GPA-based grading system ranging from A to F.", "Academics", "What is the grading system?" },
                    { 10, "You can register through the student portal during the enrollment period.", "Registration", "How do I register for courses?" },
                    { 11, "Yes, several programs offer online or hybrid learning options.", "Academics", "Can I take online courses?" },
                    { 12, "Yes, the university provides on-campus and off-campus housing options.", "Facilities", "Is hostel accommodation available?" },
                    { 13, "After admission, the university provides required documents for visa applications.", "International Students", "How do I apply for a student visa?" },
                    { 14, "The university offers a gym, swimming pool, and various sports fields.", "Facilities", "What sports facilities are available?" },
                    { 15, "Yes, students can work up to 20 hours per week on campus.", "General", "Can I work while studying?" },
                    { 16, "Student ID cards are issued by the administration office during orientation.", "General", "How do I get a student ID card?" },
                    { 17, "Use the 'Forgot Password' option on the login page.", "Technical Support", "How do I reset my student portal password?" },
                    { 18, "Exam results are typically published within three weeks after exams.", "Academics", "When will my exam results be released?" },
                    { 19, "Tuition refunds are available under specific conditions; refer to the refund policy.", "Fees", "Can I get a refund for my tuition fees?" },
                    { 20, "Some programs require internships as part of the curriculum.", "Academics", "Are internships mandatory?" },
                    { 21, "There are many clubs and organizations students can join.", "Student Life", "What extracurricular activities are available?" },
                    { 22, "Assignments can be submitted via the student portal or email, as instructed.", "Academics", "How do I submit my assignments?" },
                    { 23, "You can file a formal complaint through the student services office.", "General", "What should I do if I have a complaint?" },
                    { 24, "Yes, the university partners with several institutions worldwide.", "International Students", "Does the university offer exchange programs?" },
                    { 25, "You can reach them via email or during office hours.", "Academics", "How can I contact my professors?" },
                    { 26, "Yes, you can apply for admission deferral for up to one year.", "Admissions", "Can I defer my admission?" },
                    { 27, "Transcripts can be requested via the registrar's office.", "Academics", "How can I request my transcripts?" },
                    { 28, "Yes, there is a health center for students.", "Facilities", "Is medical assistance available on campus?" },
                    { 29, "Reach out to faculty members leading research in your field of interest.", "Academics", "How do I join a research project?" },
                    { 30, "Yes, students can apply for parking permits.", "Facilities", "Is parking available on campus?" },
                    { 31, "Certificates are issued after graduation from the registrar’s office.", "Academics", "How do I get my degree certificate?" },
                    { 32, "Yes, the university has a career center to assist students.", "Student Life", "Are there job placement services?" },
                    { 33, "Yes, student ID cards may offer discounts on public transport.", "General", "Can I use my student ID for public transport?" },
                    { 34, "Yes, the university provides multiple dining options.", "Facilities", "Is there a cafeteria on campus?" },
                    { 35, "You need to apply for a make-up exam with valid reasons.", "Academics", "What if I miss an exam?" },
                    { 36, "Advisors are assigned based on your major; details are available in the portal.", "Academics", "How do I find academic advisors?" },
                    { 37, "Only service animals are allowed in dorms and classes.", "General", "Can I bring my pet to campus?" },
                    { 38, "Yes, the university offers accessibility support services.", "Facilities", "Are there special services for disabled students?" },
                    { 39, "Lost items should be reported to campus security.", "General", "Where can I report lost items?" },
                    { 40, "Elections are organized by the student council; check the portal for dates.", "Student Life", "How do I participate in student elections?" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_answers_question_id",
                table: "answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_author_id",
                table: "articles",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_course_id",
                table: "assignments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_teacher_id",
                table: "assignments",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "ix_club_student_students_student_id",
                table: "club_student",
                column: "students_student_id");

            migrationBuilder.CreateIndex(
                name: "ix_college_teacher_teachers_teacher_id",
                table: "college_teacher",
                column: "teachers_teacher_id");

            migrationBuilder.CreateIndex(
                name: "ix_colleges_university_id",
                table: "colleges",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_syllabus_id",
                table: "course",
                column: "syllabus_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_student_students_student_id",
                table: "course_student",
                column: "students_student_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_teacher_teachers_teacher_id",
                table: "course_teacher",
                column: "teachers_teacher_id");

            migrationBuilder.CreateIndex(
                name: "ix_questions_student_id",
                table: "questions",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_results_student_id",
                table: "results",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_academic_id",
                table: "students",
                column: "academic_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_college_id",
                table: "students",
                column: "college_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_financial_id",
                table: "students",
                column: "financial_id");

            migrationBuilder.CreateIndex(
                name: "ix_submissions_assignment_id",
                table: "submissions",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "ix_submissions_student_id",
                table: "submissions",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "ix_unit_course_id",
                table: "unit",
                column: "course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropTable(
                name: "club_student");

            migrationBuilder.DropTable(
                name: "college_teacher");

            migrationBuilder.DropTable(
                name: "course_student");

            migrationBuilder.DropTable(
                name: "course_teacher");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "faqs");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "notices");

            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "submissions");

            migrationBuilder.DropTable(
                name: "unit");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "clubs");

            migrationBuilder.DropTable(
                name: "assignments");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "academics");

            migrationBuilder.DropTable(
                name: "colleges");

            migrationBuilder.DropTable(
                name: "financials");

            migrationBuilder.DropTable(
                name: "syllabus");

            migrationBuilder.DropTable(
                name: "universities");
        }
    }
}
