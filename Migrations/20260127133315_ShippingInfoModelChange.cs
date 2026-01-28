using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class ShippingInfoModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerShippingInfos",
                table: "CustomerShippingInfos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerShippingInfos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerShippingInfos",
                table: "CustomerShippingInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerShippingInfos_CustomerId",
                table: "CustomerShippingInfos",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerShippingInfos",
                table: "CustomerShippingInfos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerShippingInfos_CustomerId",
                table: "CustomerShippingInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerShippingInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerShippingInfos",
                table: "CustomerShippingInfos",
                column: "CustomerId");
        }
    }
}
