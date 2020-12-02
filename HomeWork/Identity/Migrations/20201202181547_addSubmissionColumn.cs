using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class addSubmissionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Submission",
                table: "Submissions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Submission",
                table: "Submissions");
        }
    }
}
