using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class addTableAndColumnNewSubmission2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prevSubmissionID = table.Column<int>(nullable: false),
                    ConfId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionID);
                    table.ForeignKey(
                        name: "FK_Submissions_Conferences_ConfId",
                        column: x => x.ConfId,
                        principalTable: "Conferences",
                        principalColumn: "ConfID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ConfId",
                table: "Submissions",
                column: "ConfId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");
        }
    }
}
