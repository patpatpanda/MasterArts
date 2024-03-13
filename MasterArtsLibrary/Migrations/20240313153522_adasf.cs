using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class adasf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Orders_OrderId",
                table: "Goods");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Orders_OrderId",
                table: "Goods",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Orders_OrderId",
                table: "Goods");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Goods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Orders_OrderId",
                table: "Goods",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
