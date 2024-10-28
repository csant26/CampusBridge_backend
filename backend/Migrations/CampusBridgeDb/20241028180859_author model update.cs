using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.CampusBridgeDb
{
    /// <inheritdoc />
    public partial class authormodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CampusId",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Authors");
        }
    }
}
