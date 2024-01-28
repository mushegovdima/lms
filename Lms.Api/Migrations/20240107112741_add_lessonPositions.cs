using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_lessonPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFields_Lessons_LessonId",
                table: "LessonFields");

            migrationBuilder.AddColumn<long>(
                name: "Position",
                table: "Lessons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LessonId",
                table: "LessonFields",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFields_Lessons_LessonId",
                table: "LessonFields",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFields_Lessons_LessonId",
                table: "LessonFields");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Lessons");

            migrationBuilder.AlterColumn<long>(
                name: "LessonId",
                table: "LessonFields",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFields_Lessons_LessonId",
                table: "LessonFields",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
