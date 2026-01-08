using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWPAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShelterToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Shelter_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Shelter_ShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "AspNetUsers");
        }
    }
}

