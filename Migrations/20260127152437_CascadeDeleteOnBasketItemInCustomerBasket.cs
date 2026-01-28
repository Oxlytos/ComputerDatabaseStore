using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteOnBasketItemInCustomerBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Customers_CustomerId",
                table: "BasketProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Customers_CustomerId",
                table: "BasketProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Customers_CustomerId",
                table: "BasketProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Customers_CustomerId",
                table: "BasketProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
