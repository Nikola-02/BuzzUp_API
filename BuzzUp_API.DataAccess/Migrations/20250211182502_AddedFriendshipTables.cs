using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzUp_API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedFriendshipTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendRequestStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendship_FriendRequestStatus_FriendRequestStatusId",
                        column: x => x.FriendRequestStatusId,
                        principalTable: "FriendRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFriendship",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FriendshipId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriendship", x => new { x.UserId, x.FriendshipId });
                    table.ForeignKey(
                        name: "FK_UserFriendship_Friendship_FriendshipId",
                        column: x => x.FriendshipId,
                        principalTable: "Friendship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFriendship_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_FriendRequestStatusId",
                table: "Friendship",
                column: "FriendRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriendship_FriendshipId",
                table: "UserFriendship",
                column: "FriendshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFriendship");

            migrationBuilder.DropTable(
                name: "Friendship");
        }
    }
}
