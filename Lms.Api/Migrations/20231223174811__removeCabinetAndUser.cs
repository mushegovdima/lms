using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lms.Api.Migrations
{
    /// <inheritdoc />
    public partial class _removeCabinetAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Cabinets_CabinetId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonAnswers_Users_CheckerId",
                table: "LessonAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Cabinets_CabinetId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Cabinets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CabinetId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_LessonAnswers_CheckerId",
                table: "LessonAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CabinetId",
                table: "Courses");

            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CabinetId",
                table: "Lessons",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAnswers_CheckerId",
                table: "LessonAnswers",
                column: "CheckerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CabinetId",
                table: "Courses",
                column: "CabinetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Cabinets_CabinetId",
                table: "Courses",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswers_Users_CheckerId",
                table: "LessonAnswers",
                column: "CheckerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Cabinets_CabinetId",
                table: "Lessons",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
