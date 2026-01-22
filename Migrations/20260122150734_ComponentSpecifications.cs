using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class ComponentSpecifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "CPUArchitectures");

            migrationBuilder.DropTable(
                name: "CPUSockets");

            migrationBuilder.DropTable(
                name: "EnergyClasses");

            migrationBuilder.DropTable(
                name: "MemoryTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RamProfiles",
                table: "RamProfiles");

            migrationBuilder.DropColumn(
                name: "RamProfileFeaturesType",
                table: "RamProfiles");

            migrationBuilder.RenameTable(
                name: "RamProfiles",
                newName: "AllComoinentSpecifications");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AllComoinentSpecifications",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AllComoinentSpecifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AllComoinentSpecifications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AllComoinentSpecifications");

            migrationBuilder.RenameTable(
                name: "AllComoinentSpecifications",
                newName: "RamProfiles");

            migrationBuilder.AddColumn<string>(
                name: "RamProfileFeaturesType",
                table: "RamProfiles",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RamProfiles",
                table: "RamProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CPUArchitectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUArchitectureName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    CPUSocketName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUSockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergyClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergyNameClass = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoryTypeName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTypes", x => x.Id);
                });

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
        }
    }
}
