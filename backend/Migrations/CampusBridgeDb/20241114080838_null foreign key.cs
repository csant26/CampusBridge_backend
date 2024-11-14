using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    /// <inheritdoc />
    public partial class nullforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Syllabus_SyllabusId",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "SyllabusId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Syllabus_SyllabusId",
                table: "Course",
                column: "SyllabusId",
                principalTable: "Syllabus",
                principalColumn: "SyllabusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Syllabus_SyllabusId",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "SyllabusId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Syllabus_SyllabusId",
                table: "Course",
                column: "SyllabusId",
                principalTable: "Syllabus",
                principalColumn: "SyllabusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
