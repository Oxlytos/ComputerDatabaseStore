using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class RenamedChipsetAndBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Manufacturers_ManufacturerId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Vendors_VendorId",
                table: "AllParts");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "AllParts",
                newName: "ChipsetVendorId");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "AllParts",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_AllParts_VendorId",
                table: "AllParts",
                newName: "IX_AllParts_ChipsetVendorId");

            migrationBuilder.RenameIndex(
                name: "IX_AllParts_ManufacturerId",
                table: "AllParts",
                newName: "IX_AllParts_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllParts_Manufacturers_BrandId",
                table: "AllParts",
                column: "BrandId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AllParts_Vendors_ChipsetVendorId",
                table: "AllParts",
                column: "ChipsetVendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Manufacturers_BrandId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Vendors_ChipsetVendorId",
                table: "AllParts");

            migrationBuilder.RenameColumn(
                name: "ChipsetVendorId",
                table: "AllParts",
                newName: "VendorId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "AllParts",
                newName: "ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_AllParts_ChipsetVendorId",
                table: "AllParts",
                newName: "IX_AllParts_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_AllParts_BrandId",
                table: "AllParts",
                newName: "IX_AllParts_ManufacturerId");

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
        }
    }
}
