using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class MoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId1",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_AppUserId1",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Grades",
                newName: "StudentId");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignedSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<string>(type: "TEXT", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_GroupId",
                table: "AssignedSubjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSubjects_SubjectId",
                table: "AssignedSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_AppUserId",
                table: "Lecturers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserId",
                table: "Students",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "AssignedSubjects");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Grades",
                newName: "AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Grades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AppUserId1",
                table: "Grades",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_AppUserId1",
                table: "Grades",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
