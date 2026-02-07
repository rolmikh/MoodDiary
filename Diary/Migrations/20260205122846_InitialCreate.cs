using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Emojis_EmojiId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emojis",
                table: "Emojis");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Emojis",
                newName: "Emoji");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_EmojiId",
                table: "Post",
                newName: "IX_Post_EmojiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "IdPost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emoji",
                table: "Emoji",
                column: "IdEmoji");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Emoji_EmojiId",
                table: "Post",
                column: "EmojiId",
                principalTable: "Emoji",
                principalColumn: "IdEmoji",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Emoji_EmojiId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emoji",
                table: "Emoji");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Emoji",
                newName: "Emojis");

            migrationBuilder.RenameIndex(
                name: "IX_Post_EmojiId",
                table: "Posts",
                newName: "IX_Posts_EmojiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "IdPost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emojis",
                table: "Emojis",
                column: "IdEmoji");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Emojis_EmojiId",
                table: "Posts",
                column: "EmojiId",
                principalTable: "Emojis",
                principalColumn: "IdEmoji",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
