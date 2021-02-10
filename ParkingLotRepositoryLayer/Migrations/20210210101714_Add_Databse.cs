using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class Add_Databse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverTable",
                columns: table => new
                {
                    DriverTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverType = table.Column<string>(nullable: true),
                    Charges = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTable", x => x.DriverTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTypeTable",
                columns: table => new
                {
                    ParkTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParkingType = table.Column<string>(nullable: false),
                    Charges = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTypeTable", x => x.ParkTypeID);
                });

            migrationBuilder.CreateTable(
                name: "VehicalTable",
                columns: table => new
                {
                    VehicleTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicalType = table.Column<string>(nullable: true),
                    Charges = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicalTable", x => x.VehicleTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTable",
                columns: table => new
                {
                    ParkingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicalNo = table.Column<int>(nullable: false),
                    SlotNo = table.Column<int>(nullable: false),
                    IsEmpty = table.Column<bool>(nullable: false),
                    EntryTime = table.Column<int>(nullable: false),
                    ExitTime = table.Column<int>(nullable: false),
                    Charges = table.Column<int>(nullable: false),
                    ParkTypeID = table.Column<int>(nullable: false),
                    VehicleTypeID = table.Column<int>(nullable: false),
                    DriverTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTable", x => x.ParkingId);
                    table.ForeignKey(
                        name: "FK_ParkingTable_DriverTable_DriverTypeID",
                        column: x => x.DriverTypeID,
                        principalTable: "DriverTable",
                        principalColumn: "DriverTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingTable_ParkingTypeTable_ParkTypeID",
                        column: x => x.ParkTypeID,
                        principalTable: "ParkingTypeTable",
                        principalColumn: "ParkTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingTable_VehicalTable_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicalTable",
                        principalColumn: "VehicleTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTable_DriverTypeID",
                table: "ParkingTable",
                column: "DriverTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTable_ParkTypeID",
                table: "ParkingTable",
                column: "ParkTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTable_VehicleTypeID",
                table: "ParkingTable",
                column: "VehicleTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingTable");

            migrationBuilder.DropTable(
                name: "DriverTable");

            migrationBuilder.DropTable(
                name: "ParkingTypeTable");

            migrationBuilder.DropTable(
                name: "VehicalTable");
        }
    }
}
