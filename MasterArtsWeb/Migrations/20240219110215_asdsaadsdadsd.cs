using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsWeb.Migrations
{
    /// <inheritdoc />
    public partial class asdsaadsdadsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Goods_GoodsId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GoodsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Customer",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeFrom",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeTo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FreightService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GoodsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderReference",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickUpTimeFrom",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickUpTimeTo",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTimeFrom",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTimeTo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FreightService",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GoodsId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderReference",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickUpTimeFrom",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickUpTimeTo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GoodsId",
                table: "Orders",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Goods_GoodsId",
                table: "Orders",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
