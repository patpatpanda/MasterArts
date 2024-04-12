using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterArtsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class asdfas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportExport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InlandZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Packages = table.Column<int>(type: "int", nullable: true),
                    PackageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<float>(type: "real", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToCity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pcs = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Stackable = table.Column<bool>(type: "bit", nullable: false),
                    ShippingRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dimensions_Requests_ShippingRequestId",
                        column: x => x.ShippingRequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dimensions_ShippingRequestId",
                table: "Dimensions",
                column: "ShippingRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
