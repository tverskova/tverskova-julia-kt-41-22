using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tverskova.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartmentTeacherRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Teachers_HeadTeacherTeacherId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadTeacherTeacherId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HeadTeacherTeacherId",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadTeacherTeacherId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadTeacherTeacherId",
                table: "Departments",
                column: "HeadTeacherTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Teachers_HeadTeacherTeacherId",
                table: "Departments",
                column: "HeadTeacherTeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
