using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ariefmahendra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mst_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_price = table.Column<long>(type: "bigint", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tx_purchase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    no_invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tx_purchase", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tx_purchase_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tx_purchase_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_tx_purchase_detail_mst_product_product_id",
                        column: x => x.product_id,
                        principalTable: "mst_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tx_purchase_detail_tx_purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "tx_purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tx_purchase_detail_product_id",
                table: "tx_purchase_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tx_purchase_detail_PurchaseId",
                table: "tx_purchase_detail",
                column: "PurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tx_purchase_detail");

            migrationBuilder.DropTable(
                name: "mst_product");

            migrationBuilder.DropTable(
                name: "tx_purchase");
        }
    }
}
