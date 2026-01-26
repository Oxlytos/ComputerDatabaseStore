using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class PenoptimalFinalModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SelectedProduct",
                table: "StoreProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_ProductId",
                table: "OrderedProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_StoreProducts_ProductId",
                table: "OrderedProducts",
                column: "ProductId",
                principalTable: "StoreProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_StoreProducts_ProductId",
                table: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProducts_ProductId",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "SelectedProduct",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderedProducts");
        }
    }
}
