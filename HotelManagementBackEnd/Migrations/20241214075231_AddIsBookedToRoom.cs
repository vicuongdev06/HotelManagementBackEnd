using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementBackEnd.Migrations
{
    public partial class AddIsBookedToRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Rooms");
        }
    }
}
