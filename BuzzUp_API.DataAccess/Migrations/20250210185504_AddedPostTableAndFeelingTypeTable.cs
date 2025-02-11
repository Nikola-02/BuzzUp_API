using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuzzUp_API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostTableAndFeelingTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeelingType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeelingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisibilityTypeId = table.Column<int>(type: "int", nullable: false),
                    FeelingTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_FeelingType_FeelingTypeId",
                        column: x => x.FeelingTypeId,
                        principalTable: "FeelingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_VisibilityType_VisibilityTypeId",
                        column: x => x.VisibilityTypeId,
                        principalTable: "VisibilityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeelingType_Name_IsActive",
                table: "FeelingType",
                columns: new[] { "Name", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_FeelingTypeId",
                table: "Post",
                column: "FeelingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Location_IsActive",
                table: "Post",
                columns: new[] { "Location", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Post_Title_IsActive",
                table: "Post",
                columns: new[] { "Title", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Post_VisibilityTypeId",
                table: "Post",
                column: "VisibilityTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "FeelingType");
        }
    }
}
