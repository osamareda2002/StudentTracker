using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTracker.Repository.Data.Migrations
{
    public partial class ModifyLectureEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Level_LevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_lectures_lectures_OriginalLectureId",
                table: "lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Level_LevelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_lectures_OriginalLectureId",
                table: "lectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Level",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "ProfessorAttended",
                table: "lectures");

            migrationBuilder.RenameTable(
                name: "Level",
                newName: "Levels");

            migrationBuilder.RenameColumn(
                name: "OriginalLectureId",
                table: "lectures",
                newName: "RedundantType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levels",
                table: "Levels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Levels_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Levels_LevelId",
                table: "Students",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Levels_LevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Levels_LevelId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levels",
                table: "Levels");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "RedundantType",
                table: "lectures",
                newName: "OriginalLectureId");

            migrationBuilder.AddColumn<bool>(
                name: "ProfessorAttended",
                table: "lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level",
                table: "Level",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_lectures_OriginalLectureId",
                table: "lectures",
                column: "OriginalLectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Level_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lectures_lectures_OriginalLectureId",
                table: "lectures",
                column: "OriginalLectureId",
                principalTable: "lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Level_LevelId",
                table: "Students",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
