using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diary.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Post",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2026, 2, 7, 16, 37, 2, 404, DateTimeKind.Local).AddTicks(3055));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2026, 2, 7, 16, 37, 2, 404, DateTimeKind.Local).AddTicks(3055),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
