using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class Update_VehialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicalNo",
                table: "VehicalTable",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicalNo",
                table: "VehicalTable");
        }
    }
}
