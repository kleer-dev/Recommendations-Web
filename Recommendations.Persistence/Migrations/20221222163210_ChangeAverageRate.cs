using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAverageRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRate",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRate",
                table: "Products");
        }
    }
}
