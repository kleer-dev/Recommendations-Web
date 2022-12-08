using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CategoryId",
                table: "Reviews",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Categories_CategoryId",
                table: "Reviews",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Categories_CategoryId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CategoryId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
