using System.Dynamic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_LessonField_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ExpandoObject>(
                name: "Data",
                table: "LessonFields",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "LessonFields");
        }
    }
}
