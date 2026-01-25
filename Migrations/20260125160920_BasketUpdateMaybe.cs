using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class BasketUpdateMaybe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Customers_CustomerId",
                table: "BasketProduct");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BasketProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Customers_CustomerId",
                table: "BasketProduct",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Customers_CustomerId",
                table: "BasketProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BasketProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Customers_CustomerId",
                table: "BasketProduct",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
