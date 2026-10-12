using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Diary.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emoji",
                columns: table => new
                {
                    IdEmoji = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEmoji = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CodeEmoji = table.Column<string>(type: "text", nullable: false),
                    IsPositive = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emoji", x => x.IdEmoji);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BirthdayDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostText = table.Column<string>(type: "text", nullable: true),
                    EmojiId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.IdPost);
                    table.ForeignKey(
                        name: "FK_Post_Emoji_EmojiId",
                        column: x => x.EmojiId,
                        principalTable: "Emoji",
                        principalColumn: "IdEmoji",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_EmojiId",
                table: "Post",
                column: "EmojiId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Emoji");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
