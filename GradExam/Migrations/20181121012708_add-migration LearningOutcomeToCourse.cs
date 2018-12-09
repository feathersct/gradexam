using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class addmigrationLearningOutcomeToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "LearnOutcome",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnOutcome_CourseId",
                table: "LearnOutcome",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnOutcome_Courses_CourseId",
                table: "LearnOutcome",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnOutcome_Courses_CourseId",
                table: "LearnOutcome");

            migrationBuilder.DropIndex(
                name: "IX_LearnOutcome_CourseId",
                table: "LearnOutcome");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "LearnOutcome");
        }
    }
}
