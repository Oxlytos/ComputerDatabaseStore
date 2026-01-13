using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class GPUWattChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredWattage",
                table: "ComputerPart",
                newName: "WattageConsumption");

            migrationBuilder.AddColumn<int>(
                name: "RecommendedPSUWattage",
                table: "ComputerPart",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedPSUWattage",
                table: "ComputerPart");

            migrationBuilder.RenameColumn(
                name: "WattageConsumption",
                table: "ComputerPart",
                newName: "RequiredWattage");
        }
    }
}
