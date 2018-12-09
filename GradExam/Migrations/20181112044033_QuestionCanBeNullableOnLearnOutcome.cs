using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class QuestionCanBeNullableOnLearnOutcome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionAnswerId",
                table: "LearnOutcome",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome",
                column: "QuestionAnswerId",
                principalTable: "QuesAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionAnswerId",
                table: "LearnOutcome",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome",
                column: "QuestionAnswerId",
                principalTable: "QuesAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
