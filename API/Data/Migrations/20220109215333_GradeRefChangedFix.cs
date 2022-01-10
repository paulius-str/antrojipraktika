using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class GradeRefChangedFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Assignments_SubjectId",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Assignments_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
