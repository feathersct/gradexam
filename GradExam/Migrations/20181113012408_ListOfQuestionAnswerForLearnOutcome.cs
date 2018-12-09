using Microsoft.EntityFrameworkCore.Migrations;

namespace GradExam.Migrations
{
    public partial class ListOfQuestionAnswerForLearnOutcome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnOutcome_QuesAnswer_QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.DropIndex(
                name: "IX_LearnOutcome_QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.DropColumn(
                name: "QuestionAnswerId",
                table: "LearnOutcome");

            migrationBuilder.AddColumn<int>(
                name: "LearnOutcomeId",
                table: "QuesAnswer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuesAnswer_LearnOutcomeId",
                table: "QuesAnswer",
                column: "LearnOutcomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuesAnswer_LearnOutcome_LearnOutcomeId",
                table: "QuesAnswer",
                column: "LearnOutcomeId",
                principalTable: "LearnOutcome",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuesAnswer_LearnOutcome_LearnOutcomeId",
                table: "QuesAnswer");

            migrationBuilder.DropIndex(
                name: "IX_QuesAnswer_LearnOutcomeId",
                table: "QuesAnswer");

            migrationBuilder.DropColumn(
                name: "LearnOutcomeId",
                table: "QuesAnswer");

            migrationBuilder.AddColumn<int>(
                name: "QuestionAnswerId",
                table: "LearnOutcome",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
