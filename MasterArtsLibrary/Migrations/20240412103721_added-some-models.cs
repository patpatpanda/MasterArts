using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addedsomemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inlands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total = table.Column<float>(type: "real", nullable: false),
                    usdExchangeRate = table.Column<float>(type: "real", nullable: false),
                    eurExchangeRate = table.Column<float>(type: "real", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fuelSurcharge = table.Column<float>(type: "real", nullable: false),
                    fuelSurchargePercentage = table.Column<float>(type: "real", nullable: false),
                    co2 = table.Column<float>(type: "real", nullable: false),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inlands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sailings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vessel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoyageNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransshipmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransshipmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GatewayCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GatewayDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Closing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitTime = table.Column<int>(type: "int", nullable: false),
                    SailingId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Co2 = table.Column<double>(type: "float", nullable: false),
                    OnRequest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiResponses_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiResponses_Sailings_SailingId",
                        column: x => x.SailingId,
                        principalTable: "Sailings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculationMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Multiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_ApiResponses_ApiResponseId",
                        column: x => x.ApiResponseId,
                        principalTable: "ApiResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Totals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sum = table.Column<double>(type: "float", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApiResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Totals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Totals_ApiResponses_ApiResponseId",
                        column: x => x.ApiResponseId,
                        principalTable: "ApiResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponses_AgentId",
                table: "ApiResponses",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResponses_SailingId",
                table: "ApiResponses",
                column: "SailingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_ApiResponseId",
                table: "Rates",
                column: "ApiResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Totals_ApiResponseId",
                table: "Totals",
                column: "ApiResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inlands");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Totals");

            migrationBuilder.DropTable(
                name: "ApiResponses");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Sailings");
        }
    }
}
