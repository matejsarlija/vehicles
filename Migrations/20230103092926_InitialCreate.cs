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
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Abrv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMake", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Abrv = table.Column<string>(type: "TEXT", nullable: false),
                    VehicleMakeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VehicleModel_VehicleMake_VehicleMakeID",
                        column: x => x.VehicleMakeID,
                        principalTable: "VehicleMake",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleMakeID",
                table: "VehicleModel",
                column: "VehicleMakeID");
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
