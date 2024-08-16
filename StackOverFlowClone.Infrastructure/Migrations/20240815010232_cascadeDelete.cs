using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverFlowClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Answers_AnswerID",
                table: "Votes");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Answers_AnswerID",
                table: "Votes",
                column: "AnswerID",
                principalTable: "Answers",
                principalColumn: "AnswerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Answers_AnswerID",
                table: "Votes");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Answers_AnswerID",
                table: "Votes",
                column: "AnswerID",
                principalTable: "Answers",
                principalColumn: "AnswerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
