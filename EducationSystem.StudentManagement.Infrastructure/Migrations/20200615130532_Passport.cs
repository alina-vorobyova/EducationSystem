using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationSystem.StudentManagement.Infrastructure.Migrations
{
    public partial class Passport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "Student",
                type: "NVARCHAR(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passport",
                table: "Student");
        }
    }
}
