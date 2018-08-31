using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class logging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateUtc = table.Column<DateTime>(nullable: false),
                    LogLevel = table.Column<string>(maxLength: 20, nullable: false),
                    From = table.Column<string>(maxLength: 20, nullable: false),
                    To = table.Column<string>(maxLength: 20, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Conversation = table.Column<Guid>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
