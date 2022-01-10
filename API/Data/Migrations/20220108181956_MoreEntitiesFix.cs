using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class MoreEntitiesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_AspNetUsers_AppUserId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lecturers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Lecturers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_AspNetUsers_AppUserId",
                table: "Lecturers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_AspNetUsers_AppUserId",
                table: "Lecturers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Lecturers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Lecturers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_AspNetUsers_AppUserId",
                table: "Lecturers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
