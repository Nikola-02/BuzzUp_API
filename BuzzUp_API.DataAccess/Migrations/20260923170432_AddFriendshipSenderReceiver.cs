using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzUp_API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendshipSenderReceiver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverUserId",
                table: "Friendships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderUserId",
                table: "Friendships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ReceiverUserId",
                table: "Friendships",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_SenderUserId_ReceiverUserId",
                table: "Friendships",
                columns: new[] { "SenderUserId", "ReceiverUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_ReceiverUserId",
                table: "Friendships",
                column: "ReceiverUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_SenderUserId",
                table: "Friendships",
                column: "SenderUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_ReceiverUserId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_SenderUserId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_ReceiverUserId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_SenderUserId_ReceiverUserId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "ReceiverUserId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "Friendships");
        }
    }
}
