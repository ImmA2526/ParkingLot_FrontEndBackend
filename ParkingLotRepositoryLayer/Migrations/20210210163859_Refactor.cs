using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTable_DriverTable_DriverTypeID",
                table: "ParkingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTable_ParkingTypeTable_ParkTypeID",
                table: "ParkingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTable_VehicalTable_VehicleTypeID",
                table: "ParkingTable");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTable_DriverTypeID",
                table: "ParkingTable");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTable_ParkTypeID",
                table: "ParkingTable");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTable_VehicleTypeID",
                table: "ParkingTable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTable_DriverTable_DriverTypeID",
                table: "ParkingTable",
                column: "DriverTypeID",
                principalTable: "DriverTable",
                principalColumn: "DriverTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTable_ParkingTypeTable_ParkTypeID",
                table: "ParkingTable",
                column: "ParkTypeID",
                principalTable: "ParkingTypeTable",
                principalColumn: "ParkTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTable_VehicalTable_VehicleTypeID",
                table: "ParkingTable",
                column: "VehicleTypeID",
                principalTable: "VehicalTable",
                principalColumn: "VehicleTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
