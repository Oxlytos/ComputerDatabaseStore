using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class TheGreatSchemaReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_AllComoinentSpecifications_CPUArchitectureId",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_CPUs_AllComoinentSpecifications_SocketId",
                table: "CPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_GPUs_AllComoinentSpecifications_MemoryTypeId",
                table: "GPUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllComoinentSpecifications_CPUArchitectureId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllComoinentSpecifications_CPUSocketId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Motherboards_AllComoinentSpecifications_MemoryTypeId",
                table: "Motherboards");

            migrationBuilder.DropForeignKey(
                name: "FK_PSUs_AllComoinentSpecifications_EnergyClassId",
                table: "PSUs");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMRamProfileFeatures_AllComoinentSpecifications_SupportedRamProfilesId",
                table: "RAMRamProfileFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_RAMs_AllComoinentSpecifications_MemoryTypeId",
                table: "RAMs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllComoinentSpecifications",
                table: "AllComoinentSpecifications");

            migrationBuilder.RenameTable(
                name: "AllComoinentSpecifications",
                newName: "AllComponentSpecifcations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllComponentSpecifcations",
                table: "AllComponentSpecifcations",
                column: "Id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllComponentSpecifcations",
                table: "AllComponentSpecifcations");

            migrationBuilder.RenameTable(
                name: "AllComponentSpecifcations",
                newName: "AllComoinentSpecifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllComoinentSpecifications",
                table: "AllComoinentSpecifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_AllComoinentSpecifications_CPUArchitectureId",
                table: "CPUs",
                column: "CPUArchitectureId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CPUs_AllComoinentSpecifications_SocketId",
                table: "CPUs",
                column: "SocketId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GPUs_AllComoinentSpecifications_MemoryTypeId",
                table: "GPUs",
                column: "MemoryTypeId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllComoinentSpecifications_CPUArchitectureId",
                table: "Motherboards",
                column: "CPUArchitectureId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllComoinentSpecifications_CPUSocketId",
                table: "Motherboards",
                column: "CPUSocketId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Motherboards_AllComoinentSpecifications_MemoryTypeId",
                table: "Motherboards",
                column: "MemoryTypeId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PSUs_AllComoinentSpecifications_EnergyClassId",
                table: "PSUs",
                column: "EnergyClassId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMRamProfileFeatures_AllComoinentSpecifications_SupportedRamProfilesId",
                table: "RAMRamProfileFeatures",
                column: "SupportedRamProfilesId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RAMs_AllComoinentSpecifications_MemoryTypeId",
                table: "RAMs",
                column: "MemoryTypeId",
                principalTable: "AllComoinentSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
