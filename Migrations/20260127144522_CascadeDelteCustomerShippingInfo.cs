using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelteCustomerShippingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerShippingInfos_Customers_CustomerId",
                table: "CustomerShippingInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerShippingInfos_Customers_CustomerId",
                table: "CustomerShippingInfos",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerShippingInfos_Customers_CustomerId",
                table: "CustomerShippingInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerShippingInfos_Customers_CustomerId",
                table: "CustomerShippingInfos",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
