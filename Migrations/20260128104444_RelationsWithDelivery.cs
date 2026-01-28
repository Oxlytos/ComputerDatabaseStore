using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class RelationsWithDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryProviders_DeliveryProviderId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_CustomerOrders_CustomerOrderId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Order_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrders",
                table: "CustomerOrders");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "CustomerOrders",
                newName: "CustomerOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryProviderId",
                table: "Orders",
                newName: "IX_Orders_DeliveryProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrders_CustomerId",
                table: "CustomerOrder",
                newName: "IX_CustomerOrder_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrder",
                table: "CustomerOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrder_Customers_CustomerId",
                table: "CustomerOrder",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_CustomerOrder_CustomerOrderId",
                table: "OrderedProducts",
                column: "CustomerOrderId",
                principalTable: "CustomerOrder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryProviders_DeliveryProviderId",
                table: "Orders",
                column: "DeliveryProviderId",
                principalTable: "DeliveryProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrder_Customers_CustomerId",
                table: "CustomerOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_CustomerOrder_CustomerOrderId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryProviders_DeliveryProviderId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrder",
                table: "CustomerOrder");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "CustomerOrder",
                newName: "CustomerOrders");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryProviderId",
                table: "Order",
                newName: "IX_Order_DeliveryProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrder_CustomerId",
                table: "CustomerOrders",
                newName: "IX_CustomerOrders_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrders",
                table: "CustomerOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Customers_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryProviders_DeliveryProviderId",
                table: "Order",
                column: "DeliveryProviderId",
                principalTable: "DeliveryProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_CustomerOrders_CustomerOrderId",
                table: "OrderedProducts",
                column: "CustomerOrderId",
                principalTable: "CustomerOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Order_OrderId",
                table: "OrderedProducts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
