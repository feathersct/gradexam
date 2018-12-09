using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class AddExamAndCourseToResponses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Responses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Responses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_CourseId",
                table: "Responses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_ExamId",
                table: "Responses",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Courses_CourseId",
                table: "Responses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Exams_ExamId",
                table: "Responses",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Courses_CourseId",
                table: "Responses");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Exams_ExamId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_CourseId",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_ExamId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Responses");
        }
    }
}
