using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsWeb.Migrations
{
    /// <inheritdoc />
    public partial class sl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsigneeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConsignorId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<double>(
                name: "GrossWeight",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetWeight",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PackageType",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
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
                name: "Volume",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "VolumeUnit",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VolumetricWeight",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Consignees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsigneeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsigneeCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsigneeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consignors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsignorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ConsigneeId",
                table: "Orders",
                column: "ConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ConsignorId",
                table: "Orders",
                column: "ConsignorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GoodsId",
                table: "Orders",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Consignees_ConsigneeId",
                table: "Orders",
                column: "ConsigneeId",
                principalTable: "Consignees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Consignors_ConsignorId",
                table: "Orders",
                column: "ConsignorId",
                principalTable: "Consignors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Goods_GoodsId",
                table: "Orders",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Consignees_ConsigneeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Consignors_ConsignorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Goods_GoodsId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Consignees");

            migrationBuilder.DropTable(
                name: "Consignors");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ConsigneeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ConsignorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GoodsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConsigneeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConsignorId",
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

            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "NetWeight",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "PackageType",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "VolumeUnit",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "VolumetricWeight",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Goods");
        }
    }
}
