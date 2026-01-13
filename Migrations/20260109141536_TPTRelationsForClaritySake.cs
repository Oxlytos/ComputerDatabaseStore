using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class TPTRelationsForClaritySake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_CPUArchitectures_CPUArchitectureId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_CPUArchitectures_Motherboard_CPUArchitectureId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_CPUSockets_CPUSocketId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_CPUSockets_SocketId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_EnergyClasses_EnergyClassId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_MemoryTypes_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_MemoryTypes_Motherboard_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerPart_MemoryTypes_RAM_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMRamProfileFeatures_ComputerPart_RAMsId",
                table: "RAMRamProfileFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_CPUArchitectureId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_CPUSocketId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_EnergyClassId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_Motherboard_CPUArchitectureId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_Motherboard_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_RAM_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropIndex(
                name: "IX_ComputerPart_SocketId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Bluetooth",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "CPUArchitectureId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "CPUCache",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "CPUSocketId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Cores",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "EnergyClassId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "MemorySizeGB",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "MemorySizePerStick",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "MemorySpeed",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "MemorySpeedGhz",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Motherboard_CPUArchitectureId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Motherboard_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Motherboard_Overclockable",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Overclock",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Overclockable",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "PartType",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "RAM_MemorySpeed",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "RAM_MemoryTypeId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "RecommendedPSUWattage",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "SocketId",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Soundcard",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Threads",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Wattage",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "WattageConsumption",
                table: "ComputerPart");

            migrationBuilder.DropColumn(
                name: "Wifi",
                table: "ComputerPart");

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MemorySpeedGhz = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Cores = table.Column<int>(type: "int", nullable: true),
                    Threads = table.Column<int>(type: "int", nullable: true),
                    SocketId = table.Column<int>(type: "int", nullable: true),
                    CPUArchitectureId = table.Column<int>(type: "int", nullable: true),
                    Overclockable = table.Column<bool>(type: "bit", nullable: false),
                    CPUCache = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUs_CPUArchitectures_CPUArchitectureId",
                        column: x => x.CPUArchitectureId,
                        principalTable: "CPUArchitectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CPUs_CPUSockets_SocketId",
                        column: x => x.SocketId,
                        principalTable: "CPUSockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CPUs_ComputerPart_Id",
                        column: x => x.Id,
                        principalTable: "ComputerPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MemoryTypeId = table.Column<int>(type: "int", nullable: true),
                    MemorySizeGB = table.Column<int>(type: "int", nullable: false),
                    MemorySpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Overclock = table.Column<bool>(type: "bit", nullable: false),
                    RecommendedPSUWattage = table.Column<int>(type: "int", nullable: false),
                    WattageConsumption = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUs_ComputerPart_Id",
                        column: x => x.Id,
                        principalTable: "ComputerPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GPUs_MemoryTypes_MemoryTypeId",
                        column: x => x.MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CPUSocketId = table.Column<int>(type: "int", nullable: true),
                    CPUArchitectureId = table.Column<int>(type: "int", nullable: true),
                    MemoryTypeId = table.Column<int>(type: "int", nullable: true),
                    Overclockable = table.Column<bool>(type: "bit", nullable: false),
                    Bluetooth = table.Column<bool>(type: "bit", nullable: false),
                    Wifi = table.Column<bool>(type: "bit", nullable: false),
                    Soundcard = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motherboards_CPUArchitectures_CPUArchitectureId",
                        column: x => x.CPUArchitectureId,
                        principalTable: "CPUArchitectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motherboards_CPUSockets_CPUSocketId",
                        column: x => x.CPUSocketId,
                        principalTable: "CPUSockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motherboards_ComputerPart_Id",
                        column: x => x.Id,
                        principalTable: "ComputerPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motherboards_MemoryTypes_MemoryTypeId",
                        column: x => x.MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Wattage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PSUs_ComputerPart_Id",
                        column: x => x.Id,
                        principalTable: "ComputerPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PSUs_EnergyClasses_EnergyClassId",
                        column: x => x.EnergyClassId,
                        principalTable: "EnergyClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MemoryTypeId = table.Column<int>(type: "int", nullable: true),
                    MemorySizePerStick = table.Column<int>(type: "int", nullable: false),
                    MemorySpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAMs_ComputerPart_Id",
                        column: x => x.Id,
                        principalTable: "ComputerPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RAMs_MemoryTypes_MemoryTypeId",
                        column: x => x.MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_CPUArchitectureId",
                table: "CPUs",
                column: "CPUArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_SocketId",
                table: "CPUs",
                column: "SocketId");

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_MemoryTypeId",
                table: "GPUs",
                column: "MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_CPUArchitectureId",
                table: "Motherboards",
                column: "CPUArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_CPUSocketId",
                table: "Motherboards",
                column: "CPUSocketId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_MemoryTypeId",
                table: "Motherboards",
                column: "MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PSUs_EnergyClassId",
                table: "PSUs",
                column: "EnergyClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RAMs_MemoryTypeId",
                table: "RAMs",
                column: "MemoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RAMRamProfileFeatures_RAMs_RAMsId",
                table: "RAMRamProfileFeatures",
                column: "RAMsId",
                principalTable: "RAMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RAMRamProfileFeatures_RAMs_RAMsId",
                table: "RAMRamProfileFeatures");

            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "GPUs");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "PSUs");

            migrationBuilder.DropTable(
                name: "RAMs");

            migrationBuilder.AddColumn<bool>(
                name: "Bluetooth",
                table: "ComputerPart",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CPUArchitectureId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CPUCache",
                table: "ComputerPart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CPUSocketId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cores",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnergyClassId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemorySizeGB",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemorySizePerStick",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MemorySpeed",
                table: "ComputerPart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MemorySpeedGhz",
                table: "ComputerPart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemoryTypeId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Motherboard_CPUArchitectureId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Motherboard_MemoryTypeId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Motherboard_Overclockable",
                table: "ComputerPart",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Overclock",
                table: "ComputerPart",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Overclockable",
                table: "ComputerPart",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartType",
                table: "ComputerPart",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RAM_MemorySpeed",
                table: "ComputerPart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RAM_MemoryTypeId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedPSUWattage",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocketId",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Soundcard",
                table: "ComputerPart",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Threads",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Wattage",
                table: "ComputerPart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WattageConsumption",
                table: "ComputerPart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Wifi",
                table: "ComputerPart",
                type: "bit",
                nullable: true);

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
                name: "IX_ComputerPart_SocketId",
                table: "ComputerPart",
                column: "SocketId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_CPUArchitectures_CPUArchitectureId",
                table: "ComputerPart",
                column: "CPUArchitectureId",
                principalTable: "CPUArchitectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_CPUArchitectures_Motherboard_CPUArchitectureId",
                table: "ComputerPart",
                column: "Motherboard_CPUArchitectureId",
                principalTable: "CPUArchitectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_CPUSockets_CPUSocketId",
                table: "ComputerPart",
                column: "CPUSocketId",
                principalTable: "CPUSockets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_CPUSockets_SocketId",
                table: "ComputerPart",
                column: "SocketId",
                principalTable: "CPUSockets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_EnergyClasses_EnergyClassId",
                table: "ComputerPart",
                column: "EnergyClassId",
                principalTable: "EnergyClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_MemoryTypes_MemoryTypeId",
                table: "ComputerPart",
                column: "MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_MemoryTypes_Motherboard_MemoryTypeId",
                table: "ComputerPart",
                column: "Motherboard_MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerPart_MemoryTypes_RAM_MemoryTypeId",
                table: "ComputerPart",
                column: "RAM_MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMRamProfileFeatures_ComputerPart_RAMsId",
                table: "RAMRamProfileFeatures",
                column: "RAMsId",
                principalTable: "ComputerPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
