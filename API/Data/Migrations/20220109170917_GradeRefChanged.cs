using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class GradeRefChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Assignments_AssignmentId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Grades",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_AssignmentId",
                table: "Grades",
                newName: "IX_Grades_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Assignments_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Assignments_SubjectId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Grades",
                newName: "AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_SubjectId",
                table: "Grades",
                newName: "IX_Grades_AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Assignments_AssignmentId",
                table: "Grades",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
