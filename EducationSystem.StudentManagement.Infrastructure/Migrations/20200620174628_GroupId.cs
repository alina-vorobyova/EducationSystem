using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationSystem.StudentManagement.Infrastructure.Migrations
{
    public partial class GroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Student",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Student");
        }
    }
}
