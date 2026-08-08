using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuzzUp_API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, "Austria", null },
                    { 2, null, "Belgium", null },
                    { 3, null, "Bosnia and Herzegovina", null },
                    { 4, null, "Bulgaria", null },
                    { 5, null, "Canada", null },
                    { 6, null, "China", null },
                    { 7, null, "Croatia", null },
                    { 8, null, "Czech Republic", null },
                    { 9, null, "Denmark", null },
                    { 10, null, "Egypt", null },
                    { 11, null, "Finland", null },
                    { 12, null, "France", null },
                    { 13, null, "Germany", null },
                    { 14, null, "Greece", null },
                    { 15, null, "Hungary", null },
                    { 16, null, "India", null },
                    { 17, null, "Indonesia", null },
                    { 18, null, "Ireland", null },
                    { 19, null, "Italy", null },
                    { 20, null, "Japan", null },
                    { 21, null, "Mexico", null },
                    { 22, null, "Montenegro", null },
                    { 23, null, "Netherlands", null },
                    { 24, null, "Norway", null },
                    { 25, null, "Poland", null },
                    { 26, null, "Portugal", null },
                    { 27, null, "Romania", null },
                    { 28, null, "Russia", null },
                    { 29, null, "Serbia", null },
                    { 30, null, "Slovakia", null },
                    { 31, null, "Slovenia", null },
                    { 32, null, "Spain", null },
                    { 33, null, "Sweden", null },
                    { 34, null, "Switzerland", null },
                    { 35, null, "Turkey", null },
                    { 36, null, "Ukraine", null },
                    { 37, null, "United Arab Emirates", null },
                    { 38, null, "United Kingdom", null },
                    { 39, null, "United States", null },
                    { 40, null, "Brazil", null },
                    { 41, null, "Argentina", null },
                    { 42, null, "South Africa", null },
                    { 43, null, "Nigeria", null },
                    { 44, null, "Saudi Arabia", null },
                    { 45, null, "South Korea", null },
                    { 46, null, "Thailand", null },
                    { 47, null, "Vietnam", null },
                    { 48, null, "Philippines", null },
                    { 49, null, "Pakistan", null },
                    { 50, null, "Bangladesh", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name_IsActive",
                table: "Countries",
                columns: new[] { "Name", "IsActive" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
