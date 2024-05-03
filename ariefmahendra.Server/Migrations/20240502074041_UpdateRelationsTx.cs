using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ariefmahendra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsTx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tx_purchase_detail_tx_purchase_PurchaseId",
                table: "tx_purchase_detail");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "tx_purchase_detail",
                newName: "purchase_id");

            migrationBuilder.RenameIndex(
                name: "IX_tx_purchase_detail_PurchaseId",
                table: "tx_purchase_detail",
                newName: "IX_tx_purchase_detail_purchase_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "purchase_id",
                table: "tx_purchase_detail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tx_purchase_detail_tx_purchase_purchase_id",
                table: "tx_purchase_detail",
                column: "purchase_id",
                principalTable: "tx_purchase",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tx_purchase_detail_tx_purchase_purchase_id",
                table: "tx_purchase_detail");

            migrationBuilder.RenameColumn(
                name: "purchase_id",
                table: "tx_purchase_detail",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_tx_purchase_detail_purchase_id",
                table: "tx_purchase_detail",
                newName: "IX_tx_purchase_detail_PurchaseId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseId",
                table: "tx_purchase_detail",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tx_purchase_detail_tx_purchase_PurchaseId",
                table: "tx_purchase_detail",
                column: "PurchaseId",
                principalTable: "tx_purchase",
                principalColumn: "id");
        }
    }
}
