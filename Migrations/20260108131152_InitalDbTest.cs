using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitalDbTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPUArchitectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUArchitectureName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUArchitectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CPUSockets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUSocketName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUSockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerShippingInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergyClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergyNameClass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoryTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RamProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RamProfileFeaturesType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overclockable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RamProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delivered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerShippingInfos",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State_Or_County_Or_Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerShippingInfos", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerShippingInfos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComputerPart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    VendorId = table.Column<int>(type: "int", nullable: true),
                    PartType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    MemorySpeedGhz = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cores = table.Column<int>(type: "int", nullable: true),
                    Threads = table.Column<int>(type: "int", nullable: true),
                    SocketId = table.Column<int>(type: "int", nullable: true),
                    CPUArchitectureId = table.Column<int>(type: "int", nullable: true),
                    Overclockable = table.Column<bool>(type: "bit", nullable: true),
                    CPUCache = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MemoryTypeId = table.Column<int>(type: "int", nullable: true),
                    MemorySizeGB = table.Column<int>(type: "int", nullable: true),
                    MemorySpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Overclock = table.Column<bool>(type: "bit", nullable: true),
                    RequiredWattage = table.Column<int>(type: "int", nullable: true),
                    CPUSocketId = table.Column<int>(type: "int", nullable: true),
                    Motherboard_CPUArchitectureId = table.Column<int>(type: "int", nullable: true),
                    Motherboard_MemoryTypeId = table.Column<int>(type: "int", nullable: true),
                    Motherboard_Overclockable = table.Column<bool>(type: "bit", nullable: true),
                    Bluetooth = table.Column<bool>(type: "bit", nullable: true),
                    Wifi = table.Column<bool>(type: "bit", nullable: true),
                    Soundcard = table.Column<bool>(type: "bit", nullable: true),
                    Wattage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnergyClassId = table.Column<int>(type: "int", nullable: true),
                    RAM_MemoryTypeId = table.Column<int>(type: "int", nullable: true),
                    MemorySizePerStick = table.Column<int>(type: "int", nullable: true),
                    RAM_MemorySpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RamProfileFeaturesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerPart_CPUArchitectures_CPUArchitectureId",
                        column: x => x.CPUArchitectureId,
                        principalTable: "CPUArchitectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_CPUArchitectures_Motherboard_CPUArchitectureId",
                        column: x => x.Motherboard_CPUArchitectureId,
                        principalTable: "CPUArchitectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_CPUSockets_CPUSocketId",
                        column: x => x.CPUSocketId,
                        principalTable: "CPUSockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_CPUSockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "CPUSockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_EnergyClasses_EnergyClassId",
                        column: x => x.EnergyClassId,
                        principalTable: "EnergyClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ComputerPart_MemoryTypes_MemoryTypeId",
                        column: x => x.MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_MemoryTypes_Motherboard_MemoryTypeId",
                        column: x => x.Motherboard_MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_MemoryTypes_RAM_MemoryTypeId",
                        column: x => x.RAM_MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_RamProfiles_RamProfileFeaturesId",
                        column: x => x.RamProfileFeaturesId,
                        principalTable: "RamProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComputerPart_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderedAmount = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CustomerOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedProducts_CustomerOrders_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderedProducts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PartTypeId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sale = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProducts_ComputerPart_PartTypeId",
                        column: x => x.PartTypeId,
                        principalTable: "ComputerPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_CPUArchitectureId",
                table: "ComputerPart",
                column: "CPUArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_CPUSocketId",
                table: "ComputerPart",
                column: "CPUSocketId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_EnergyClassId",
                table: "ComputerPart",
                column: "EnergyClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_ManufacturerId",
                table: "ComputerPart",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_MemoryTypeId",
                table: "ComputerPart",
                column: "MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_Motherboard_CPUArchitectureId",
                table: "ComputerPart",
                column: "Motherboard_CPUArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_Motherboard_MemoryTypeId",
                table: "ComputerPart",
                column: "Motherboard_MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_RAM_MemoryTypeId",
                table: "ComputerPart",
                column: "RAM_MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_RamProfileFeaturesId",
                table: "ComputerPart",
                column: "RamProfileFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_SocketId",
                table: "ComputerPart",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerPart_VendorId",
                table: "ComputerPart",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_CustomerId",
                table: "OrderedProducts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_CustomerOrderId",
                table: "OrderedProducts",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ManufacturerId",
                table: "StoreProducts",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_PartTypeId",
                table: "StoreProducts",
                column: "PartTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerShippingInfos");

            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "ComputerPart");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CPUArchitectures");

            migrationBuilder.DropTable(
                name: "CPUSockets");

            migrationBuilder.DropTable(
                name: "EnergyClasses");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "MemoryTypes");

            migrationBuilder.DropTable(
                name: "RamProfiles");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
