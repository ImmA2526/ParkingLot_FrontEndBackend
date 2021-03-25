using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class parking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverTypeTable",
                columns: table => new
                {
                    DriverTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverType = table.Column<string>(nullable: true),
                    Charges = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTypeTable", x => x.DriverTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTable",
                columns: table => new
                {
                    ParkingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicalNo = table.Column<string>(nullable: false),
                    SlotNo = table.Column<int>(nullable: false),
                    IsEmpty = table.Column<bool>(nullable: false),
                    EntryTime = table.Column<DateTime>(nullable: false),
                    ExitTime = table.Column<DateTime>(nullable: false),
                    Charges = table.Column<int>(nullable: false),
                    ParkingTypeID = table.Column<int>(nullable: false),
                    VehicleTypeID = table.Column<int>(nullable: false),
                    DriverTypeID = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTable", x => x.ParkingId);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTypeTable",
                columns: table => new
                {
                    ParkingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParkingType = table.Column<string>(nullable: false),
                    Charges = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTypeTable", x => x.ParkingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "VehicalTypeTable",
                columns: table => new
                {
                    VehicleTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicalType = table.Column<string>(nullable: true),
                    Charges = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicalTypeTable", x => x.VehicleTypeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverTypeTable");

            migrationBuilder.DropTable(
                name: "ParkingTable");

            migrationBuilder.DropTable(
                name: "ParkingTypeTable");

            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.DropTable(
                name: "VehicalTypeTable");
        }
    }
}
