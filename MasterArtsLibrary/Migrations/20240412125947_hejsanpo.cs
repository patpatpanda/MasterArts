using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class hejsanpo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerOrderNumber",
                table: "CustomerRates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerOrderNumber",
                table: "CustomerRates");
        }
    }
}
