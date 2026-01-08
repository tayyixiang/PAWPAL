using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWPAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Species",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pet");
        }
    }
}
