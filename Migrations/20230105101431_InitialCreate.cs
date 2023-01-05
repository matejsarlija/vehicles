using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleMake",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Abrv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMake", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VehicleMakeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Abrv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModel_VehicleMake_VehicleMakeId",
                        column: x => x.VehicleMakeId,
                        principalTable: "VehicleMake",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleMakeId",
                table: "VehicleModel",
                column: "VehicleMakeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleModel");

            migrationBuilder.DropTable(
                name: "VehicleMake");
        }
    }
}
