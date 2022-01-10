using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class GradeToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Grades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AppUserId",
                table: "Grades",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId",
                table: "Grades",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AppUserId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Grades");
        }
    }
}
