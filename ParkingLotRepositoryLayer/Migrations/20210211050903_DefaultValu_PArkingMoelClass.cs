using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class DefaultValu_PArkingMoelClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicalNo",
                table: "VehicalTable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicalNo",
                table: "VehicalTable",
                nullable: false,
                defaultValue: 0);
        }
    }
}
