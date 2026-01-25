using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class CustomerStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "StoreProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_CustomerId",
                table: "StoreProducts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Customers_CustomerId",
                table: "StoreProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Customers_CustomerId",
                table: "StoreProducts");

            migrationBuilder.DropIndex(
                name: "IX_StoreProducts_CustomerId",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "StoreProducts");
        }
    }
}
