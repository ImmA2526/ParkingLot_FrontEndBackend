using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class AutoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserTable",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserTable",
                newName: "id");
        }
    }
}
