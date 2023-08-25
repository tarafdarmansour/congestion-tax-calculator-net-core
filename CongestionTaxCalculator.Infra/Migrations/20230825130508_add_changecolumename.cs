using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CongestionTaxCalculator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class add_changecolumename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdjustedMovementInterval",
                table: "Cities",
                newName: "MovementIntervalInMinute");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovementIntervalInMinute",
                table: "Cities",
                newName: "AdjustedMovementInterval");
        }
    }
}
