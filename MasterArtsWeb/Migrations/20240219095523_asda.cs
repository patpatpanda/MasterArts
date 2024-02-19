using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsWeb.Migrations
{
    /// <inheritdoc />
    public partial class asda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleCode",
                table: "Goods",
                newName: "VolumeUnit");

            migrationBuilder.AddColumn<double>(
                name: "GrossWeight",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "VolumeUnit",
                table: "Goods",
                newName: "ArticleCode");
        }
    }
}
