using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class StockUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_Manufacturers_ManufacturerId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_Vendors_VendorId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_ComputerPart_Id",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_GPUs_ComputerPart_Id",
                table: "GPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_ComputerPart_Id",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_PSUs_ComputerPart_Id",
                table: "PSUs");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMs_ComputerPart_Id",
                table: "RAMs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_ComputerPart_PartTypeId",
                table: "StoreProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComputerPart",
                table: "ComputerPart");

            migrationBuilder.RenameTable(
                name: "ComputerPart",
                newName: "AllParts");

            migrationBuilder.RenameIndex(
                name: "IX_ComputerPart_VendorId",
                table: "AllParts",
                newName: "IX_AllParts_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_ComputerPart_ManufacturerId",
                table: "AllParts",
                newName: "IX_AllParts_ManufacturerId");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "AllParts",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllParts",
                table: "AllParts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllParts_Manufacturers_ManufacturerId",
                table: "AllParts",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AllParts_Vendors_VendorId",
                table: "AllParts",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_AllParts_Id",
                table: "CPUs",
                column: "Id",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GPUs_AllParts_Id",
                table: "GPUs",
                column: "Id",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllParts_Id",
                table: "Motherboards",
                column: "Id",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PSUs_AllParts_Id",
                table: "PSUs",
                column: "Id",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMs_AllParts_Id",
                table: "RAMs",
                column: "Id",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_AllParts_PartTypeId",
                table: "StoreProducts",
                column: "PartTypeId",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Manufacturers_ManufacturerId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Vendors_VendorId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_AllParts_Id",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_GPUs_AllParts_Id",
                table: "GPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllParts_Id",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_PSUs_AllParts_Id",
                table: "PSUs");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMs_AllParts_Id",
                table: "RAMs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_AllParts_PartTypeId",
                table: "StoreProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllParts",
                table: "AllParts");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "AllParts");

            migrationBuilder.RenameTable(
                name: "AllParts",
                newName: "ComputerPart");

            migrationBuilder.RenameIndex(
                name: "IX_AllParts_VendorId",
                table: "ComputerPart",
                newName: "IX_ComputerPart_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_AllParts_ManufacturerId",
                table: "ComputerPart",
                newName: "IX_ComputerPart_ManufacturerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComputerPart",
                table: "ComputerPart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_Manufacturers_ManufacturerId",
                table: "ComputerPart",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_Vendors_VendorId",
                table: "ComputerPart",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_ComputerPart_Id",
                table: "CPUs",
                column: "Id",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GPUs_ComputerPart_Id",
                table: "GPUs",
                column: "Id",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_ComputerPart_Id",
                table: "Motherboards",
                column: "Id",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PSUs_ComputerPart_Id",
                table: "PSUs",
                column: "Id",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMs_ComputerPart_Id",
                table: "RAMs",
                column: "Id",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_ComputerPart_PartTypeId",
                table: "StoreProducts",
                column: "PartTypeId",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
