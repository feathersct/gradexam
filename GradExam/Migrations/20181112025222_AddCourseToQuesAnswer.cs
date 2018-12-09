using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class AddCourseToQuesAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Course_id",
                table: "QuesAnswer",
                newName: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuesAnswer_CourseId",
                table: "QuesAnswer",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuesAnswer_Courses_CourseId",
                table: "QuesAnswer",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuesAnswer_Courses_CourseId",
                table: "QuesAnswer");

            migrationBuilder.DropIndex(
                name: "IX_QuesAnswer_CourseId",
                table: "QuesAnswer");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "QuesAnswer",
                newName: "Course_id");
        }
    }
}
