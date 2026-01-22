using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class NameOnAbstractSpecifcation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_AllComponentSpecifcations_CPUArchitectureId",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_AllComponentSpecifcations_SocketId",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_GPUs_AllComponentSpecifcations_MemoryTypeId",
                table: "GPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllComponentSpecifcations_CPUArchitectureId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllComponentSpecifcations_CPUSocketId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllComponentSpecifcations_MemoryTypeId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_PSUs_AllComponentSpecifcations_EnergyClassId",
                table: "PSUs");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMRamProfileFeatures_AllComponentSpecifcations_SupportedRamProfilesId",
                table: "RAMRamProfileFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMs_AllComponentSpecifcations_MemoryTypeId",
                table: "RAMs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_AllParts_PartTypeId",
                table: "StoreProducts");

            migrationBuilder.DropIndex(
                name: "IX_StoreProducts_PartTypeId",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "PartTypeId",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AllComponentSpecifcations");

            migrationBuilder.AddColumn<int>(
                name: "ComputerPartId",
                table: "StoreProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StoreProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AllComponentSpecifcations",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CPUArchitectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUArchitectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUArchitectures_AllComponentSpecifcations_Id",
                        column: x => x.Id,
                        principalTable: "AllComponentSpecifcations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUSockets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUSockets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUSockets_AllComponentSpecifcations_Id",
                        column: x => x.Id,
                        principalTable: "AllComponentSpecifcations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyClasses_AllComponentSpecifcations_Id",
                        column: x => x.Id,
                        principalTable: "AllComponentSpecifcations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoryTypes_AllComponentSpecifcations_Id",
                        column: x => x.Id,
                        principalTable: "AllComponentSpecifcations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RamProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RamProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RamProfiles_AllComponentSpecifcations_Id",
                        column: x => x.Id,
                        principalTable: "AllComponentSpecifcations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ComputerPartId",
                table: "StoreProducts",
                column: "ComputerPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_CPUArchitectures_CPUArchitectureId",
                table: "CPUs",
                column: "CPUArchitectureId",
                principalTable: "CPUArchitectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_CPUSockets_SocketId",
                table: "CPUs",
                column: "SocketId",
                principalTable: "CPUSockets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GPUs_MemoryTypes_MemoryTypeId",
                table: "GPUs",
                column: "MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_CPUArchitectures_CPUArchitectureId",
                table: "Motherboards",
                column: "CPUArchitectureId",
                principalTable: "CPUArchitectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_CPUSockets_CPUSocketId",
                table: "Motherboards",
                column: "CPUSocketId",
                principalTable: "CPUSockets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_MemoryTypes_MemoryTypeId",
                table: "Motherboards",
                column: "MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PSUs_EnergyClasses_EnergyClassId",
                table: "PSUs",
                column: "EnergyClassId",
                principalTable: "EnergyClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMRamProfileFeatures_RamProfiles_SupportedRamProfilesId",
                table: "RAMRamProfileFeatures",
                column: "SupportedRamProfilesId",
                principalTable: "RamProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMs_MemoryTypes_MemoryTypeId",
                table: "RAMs",
                column: "MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_AllParts_ComputerPartId",
                table: "StoreProducts",
                column: "ComputerPartId",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_CPUArchitectures_CPUArchitectureId",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_CPUSockets_SocketId",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_GPUs_MemoryTypes_MemoryTypeId",
                table: "GPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_CPUArchitectures_CPUArchitectureId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_CPUSockets_CPUSocketId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_MemoryTypes_MemoryTypeId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_PSUs_EnergyClasses_EnergyClassId",
                table: "PSUs");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMRamProfileFeatures_RamProfiles_SupportedRamProfilesId",
                table: "RAMRamProfileFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMs_MemoryTypes_MemoryTypeId",
                table: "RAMs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_AllParts_ComputerPartId",
                table: "StoreProducts");

            migrationBuilder.DropTable(
                name: "CPUArchitectures");

            migrationBuilder.DropTable(
                name: "CPUSockets");

            migrationBuilder.DropTable(
                name: "EnergyClasses");

            migrationBuilder.DropTable(
                name: "MemoryTypes");

            migrationBuilder.DropTable(
                name: "RamProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StoreProducts_ComputerPartId",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "ComputerPartId",
                table: "StoreProducts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StoreProducts");

            migrationBuilder.AddColumn<int>(
                name: "PartTypeId",
                table: "StoreProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AllComponentSpecifcations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AllComponentSpecifcations",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_PartTypeId",
                table: "StoreProducts",
                column: "PartTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_AllComponentSpecifcations_CPUArchitectureId",
                table: "CPUs",
                column: "CPUArchitectureId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_AllComponentSpecifcations_SocketId",
                table: "CPUs",
                column: "SocketId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GPUs_AllComponentSpecifcations_MemoryTypeId",
                table: "GPUs",
                column: "MemoryTypeId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllComponentSpecifcations_CPUArchitectureId",
                table: "Motherboards",
                column: "CPUArchitectureId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllComponentSpecifcations_CPUSocketId",
                table: "Motherboards",
                column: "CPUSocketId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllComponentSpecifcations_MemoryTypeId",
                table: "Motherboards",
                column: "MemoryTypeId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PSUs_AllComponentSpecifcations_EnergyClassId",
                table: "PSUs",
                column: "EnergyClassId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMRamProfileFeatures_AllComponentSpecifcations_SupportedRamProfilesId",
                table: "RAMRamProfileFeatures",
                column: "SupportedRamProfilesId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMs_AllComponentSpecifcations_MemoryTypeId",
                table: "RAMs",
                column: "MemoryTypeId",
                principalTable: "AllComponentSpecifcations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_AllParts_PartTypeId",
                table: "StoreProducts",
                column: "PartTypeId",
                principalTable: "AllParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
