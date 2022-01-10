using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class GradeToUserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Assignments_AssignmentId1",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AppUserId1",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AppUserId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AssignmentId1",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "AssignmentId1",
                table: "Grades");

            migrationBuilder.AlterColumn<int>(
                name: "AssignmentId",
                table: "Grades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Grades",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");


            migrationBuilder.CreateIndex(
                name: "IX_Grades_AssignmentId",
                table: "Grades",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Assignments_AssignmentId",
                table: "Grades",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId1",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Assignments_AssignmentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AppUserId1",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AssignmentId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Grades");

            migrationBuilder.AlterColumn<string>(
                name: "AssignmentId",
                table: "Grades",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Grades",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AppUserId",
                table: "Grades",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AssignmentId1",
                table: "Grades",
                column: "AssignmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId",
                table: "Grades",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Assignments_AssignmentId1",
                table: "Grades",
                column: "AssignmentId1",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
