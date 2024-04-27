using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class AddCloumnInTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name:"latitude",
                table:"Trip",
                type:"DECIMAL(12,9)",
                nullable  :false ,
                 defaultValue :0.0m
                );
            migrationBuilder.AddColumn<decimal>(
                name: "longitude",
                table: "Trip",
                type: "DECIMAL(12,9)",
                nullable: false,
                 defaultValue: 0.0m
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
