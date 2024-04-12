using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class lolbollaen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerRatesId",
                table: "Totals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerRatesId",
                table: "Rates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitTime = table.Column<int>(type: "int", nullable: true),
                    SailingId = table.Column<int>(type: "int", nullable: true),
                    AgentId = table.Column<int>(type: "int", nullable: true),
                    ValidFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co2 = table.Column<double>(type: "float", nullable: true),
                    OnRequest = table.Column<bool>(type: "bit", nullable: true),
                    ShippingRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerRates_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerRates_Requests_ShippingRequestId",
                        column: x => x.ShippingRequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerRates_Sailings_SailingId",
                        column: x => x.SailingId,
                        principalTable: "Sailings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Totals_CustomerRatesId",
                table: "Totals",
                column: "CustomerRatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CustomerRatesId",
                table: "Rates",
                column: "CustomerRatesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRates_AgentId",
                table: "CustomerRates",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRates_SailingId",
                table: "CustomerRates",
                column: "SailingId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRates_ShippingRequestId",
                table: "CustomerRates",
                column: "ShippingRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_CustomerRates_CustomerRatesId",
                table: "Rates",
                column: "CustomerRatesId",
                principalTable: "CustomerRates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Totals_CustomerRates_CustomerRatesId",
                table: "Totals",
                column: "CustomerRatesId",
                principalTable: "CustomerRates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_CustomerRates_CustomerRatesId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Totals_CustomerRates_CustomerRatesId",
                table: "Totals");

            migrationBuilder.DropTable(
                name: "CustomerRates");

            migrationBuilder.DropIndex(
                name: "IX_Totals_CustomerRatesId",
                table: "Totals");

            migrationBuilder.DropIndex(
                name: "IX_Rates_CustomerRatesId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "CustomerRatesId",
                table: "Totals");

            migrationBuilder.DropColumn(
                name: "CustomerRatesId",
                table: "Rates");
        }
    }
}
