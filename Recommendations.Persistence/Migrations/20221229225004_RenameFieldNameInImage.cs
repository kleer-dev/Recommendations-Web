using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommendations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameFieldNameInImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Images",
                newName: "FolderName");

            migrationBuilder.RenameColumn(
                name: "Folder",
                table: "Images",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FolderName",
                table: "Images",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "Folder");
        }
    }
}
