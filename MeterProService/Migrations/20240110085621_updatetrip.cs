using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class updatetrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
              name: "cabId",
            table: "Trip", // Replace with your actual table name
            nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
