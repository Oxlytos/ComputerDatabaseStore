using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class DbSetRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Manufacturers_BrandId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_Vendors_ChipsetVendorId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Manufacturers_ManufacturerId",
                table: "StoreProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "ChipsetVendors");

            migrationBuilder.RenameTable(
                name: "Manufacturers",
                newName: "BrandManufacturers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChipsetVendors",
                table: "ChipsetVendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrandManufacturers",
                table: "BrandManufacturers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllParts_BrandManufacturers_BrandId",
                table: "AllParts",
                column: "BrandId",
                principalTable: "BrandManufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AllParts_ChipsetVendors_ChipsetVendorId",
                table: "AllParts",
                column: "ChipsetVendorId",
                principalTable: "ChipsetVendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_BrandManufacturers_ManufacturerId",
                table: "StoreProducts",
                column: "ManufacturerId",
                principalTable: "BrandManufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_BrandManufacturers_BrandId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_AllParts_ChipsetVendors_ChipsetVendorId",
                table: "AllParts");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_BrandManufacturers_ManufacturerId",
                table: "StoreProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChipsetVendors",
                table: "ChipsetVendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrandManufacturers",
                table: "BrandManufacturers");

            migrationBuilder.RenameTable(
                name: "ChipsetVendors",
                newName: "Vendors");

            migrationBuilder.RenameTable(
                name: "BrandManufacturers",
                newName: "Manufacturers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Manufacturers_ManufacturerId",
                table: "StoreProducts",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
