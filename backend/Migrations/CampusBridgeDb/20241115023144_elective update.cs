using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    /// <inheritdoc />
    public partial class electiveupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedElectiveNo",
                table: "Syllabus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Batch",
                table: "Academics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Semester",
                table: "Academics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedElectiveNo",
                table: "Syllabus");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Academics");

            migrationBuilder.AlterColumn<int>(
                name: "Batch",
                table: "Academics",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
