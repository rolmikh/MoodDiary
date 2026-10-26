using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Diary.Migrations
{
    /// <inheritdoc />
    public partial class AddUserVerificationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post");

            migrationBuilder.CreateTable(
                name: "UserVerificationToken",
                columns: table => new
                {
                    IdUserVerificationToken = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HashData = table.Column<string>(type: "text", nullable: false),
                    OperationType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVerificationToken", x => x.IdUserVerificationToken);
                    table.ForeignKey(
                        name: "FK_UserVerificationToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationToken_UserId",
                table: "UserVerificationToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "User",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "UserVerificationToken");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "User",
                principalColumn: "IdUser");
        }
    }
}
