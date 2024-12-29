using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTracker.Repository.Data.Migrations
{
    public partial class RelateDepartmentWithLectures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_lectures_DeptId",
                table: "lectures",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_lectures_Departments_DeptId",
                table: "lectures",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lectures_Departments_DeptId",
                table: "lectures");

            migrationBuilder.DropIndex(
                name: "IX_lectures_DeptId",
                table: "lectures");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "lectures");
        }
    }
}
