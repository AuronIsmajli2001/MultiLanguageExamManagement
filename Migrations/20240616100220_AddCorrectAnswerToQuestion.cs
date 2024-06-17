using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiLanguageExamManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectAnswerToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Users_CreatorId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenExams_Exams_ExamId",
                table: "TakenExams");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenExams_Users_UserId",
                table: "TakenExams");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Users_CreatorId",
                table: "Exams",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenExams_Exams_ExamId",
                table: "TakenExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TakenExams_Users_UserId",
                table: "TakenExams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Users_CreatorId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenExams_Exams_ExamId",
                table: "TakenExams");

            migrationBuilder.DropForeignKey(
                name: "FK_TakenExams_Users_UserId",
                table: "TakenExams");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Users_CreatorId",
                table: "Exams",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenExams_Exams_ExamId",
                table: "TakenExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakenExams_Users_UserId",
                table: "TakenExams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
