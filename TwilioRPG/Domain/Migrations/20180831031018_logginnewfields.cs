using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class logginnewfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "Logs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Logs",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Controller",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Logs");
        }
    }
}
