using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationReviewRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RatingId",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RatingId",
                table: "Reviews",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Ratings_RatingId",
                table: "Reviews",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Ratings_RatingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RatingId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Reviews");
        }
    }
}
