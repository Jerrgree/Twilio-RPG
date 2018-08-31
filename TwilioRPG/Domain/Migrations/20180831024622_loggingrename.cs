using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class loggingrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "Logs",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Logs",
                newName: "Destination");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Source",
                table: "Logs",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Logs",
                newName: "From");
        }
    }
}
