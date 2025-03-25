using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tverskova.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHeadTeacherRelationship3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "HeadTeacherTeacherId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers",
                column: "DepartmentId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Teachers_HeadTeacherTeacherId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadTeacherTeacherId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HeadTeacherTeacherId",
                table: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentId",
                table: "Teachers",
                column: "DepartmentId",
                unique: true);
        }
    }
}
