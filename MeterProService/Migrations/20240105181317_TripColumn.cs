using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class TripColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "strtlocation",
             table: "Trip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
