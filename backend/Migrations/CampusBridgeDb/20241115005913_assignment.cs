using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    /// <inheritdoc />
    public partial class assignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MajorStudent");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isElective",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesCourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentsStudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesCourseId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Course_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AssignmentId",
                table: "Images",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SubmissionId",
                table: "Images",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsStudentId",
                table: "CourseStudent",
                column: "StudentsStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Assignments_AssignmentId",
                table: "Images",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Submissions_SubmissionId",
                table: "Images",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "SubmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Assignments_AssignmentId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Submissions_SubmissionId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Images_AssignmentId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SubmissionId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "isElective",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorId);
                });

            migrationBuilder.CreateTable(
                name: "MajorStudent",
                columns: table => new
                {
                    MajorsMajorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentsStudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorStudent", x => new { x.MajorsMajorId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_MajorStudent_Majors_MajorsMajorId",
                        column: x => x.MajorsMajorId,
                        principalTable: "Majors",
                        principalColumn: "MajorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MajorStudent_Students_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MajorStudent_StudentsStudentId",
                table: "MajorStudent",
                column: "StudentsStudentId");
        }
    }
}
