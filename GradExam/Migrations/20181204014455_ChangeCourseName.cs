using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class ChangeCourseName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Courses_CourseId",
                table: "Responses");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Responses",
                newName: "Course1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_CourseId",
                table: "Responses",
                newName: "IX_Responses_Course1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Courses_Course1Id",
                table: "Responses",
                column: "Course1Id",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Courses_Course1Id",
                table: "Responses");

            migrationBuilder.RenameColumn(
                name: "Course1Id",
                table: "Responses",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Responses_Course1Id",
                table: "Responses",
                newName: "IX_Responses_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Courses_CourseId",
                table: "Responses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
