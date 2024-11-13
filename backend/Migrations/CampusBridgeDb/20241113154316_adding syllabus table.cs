using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    /// <inheritdoc />
    public partial class addingsyllabustable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Syllabus",
                columns: table => new
                {
                    SyllabusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syllabus", x => x.SyllabusId);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseObjective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassMarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHour = table.Column<int>(type: "int", nullable: false),
                    LabDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Books = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyllabusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_Syllabus_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabus",
                        principalColumn: "SyllabusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionHours = table.Column<int>(type: "int", nullable: false),
                    SubUnits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Unit_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_SyllabusId",
                table: "Course",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_CourseId",
                table: "Unit",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Syllabus");
        }
    }
}
