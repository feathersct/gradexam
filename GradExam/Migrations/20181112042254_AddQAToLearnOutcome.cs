using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class AddQAToLearnOutcome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuesAnswer_Courses_CourseId",
                table: "QuesAnswer");

            migrationBuilder.RenameColumn(
                name: "Question_id",
                table: "LearnOutcome",
                newName: "QuestionAnswerId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "QuesAnswer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_LearnOutcome_QuestionAnswerId",
                table: "LearnOutcome",
                column: "QuestionAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome",
                column: "QuestionAnswerId",
                principalTable: "QuesAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuesAnswer_Courses_CourseId",
                table: "QuesAnswer",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.DropForeignKey(
                name: "FK_QuesAnswer_Courses_CourseId",
                table: "QuesAnswer");

            migrationBuilder.DropIndex(
                name: "IX_LearnOutcome_QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.RenameColumn(
                name: "QuestionAnswerId",
                table: "LearnOutcome",
                newName: "Question_id");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "QuesAnswer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuesAnswer_Courses_CourseId",
                table: "QuesAnswer",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
