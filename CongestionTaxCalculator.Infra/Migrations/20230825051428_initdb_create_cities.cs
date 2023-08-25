using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CongestionTaxCalculator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class initdb_create_cities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayMaxTax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayPeriodTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TaxFee = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayPeriodTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayPeriodTax_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FreeChargeDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeOfChargeDate = table.Column<DateTime>(type: "date", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeChargeDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeChargeDate_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FreeChargeDayOfWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeChargeDayOfWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeChargeDayOfWeek_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FreeChargeMonth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeChargeMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreeChargeMonth_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayPeriodTax_CityId",
                table: "DayPeriodTax",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_FreeChargeDate_CityId",
                table: "FreeChargeDate",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_FreeChargeDayOfWeek_CityId",
                table: "FreeChargeDayOfWeek",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_FreeChargeMonth_CityId",
                table: "FreeChargeMonth",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayPeriodTax");

            migrationBuilder.DropTable(
                name: "FreeChargeDate");

            migrationBuilder.DropTable(
                name: "FreeChargeDayOfWeek");

            migrationBuilder.DropTable(
                name: "FreeChargeMonth");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
