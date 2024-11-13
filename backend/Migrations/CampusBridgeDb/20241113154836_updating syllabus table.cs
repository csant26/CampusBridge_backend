using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    /// <inheritdoc />
    public partial class updatingsyllabustable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Syllabus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Syllabus");
        }
    }
}
