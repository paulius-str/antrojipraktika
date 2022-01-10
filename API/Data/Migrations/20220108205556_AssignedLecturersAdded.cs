using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AssignedLecturersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignedLecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    LecturerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedLecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedLecturers_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedLecturers_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedLecturers_LecturerId",
                table: "AssignedLecturers",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedLecturers_SubjectId",
                table: "AssignedLecturers",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedLecturers");
        }
    }
}
