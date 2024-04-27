using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class TripTableModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "cabId",
            table: "Trip",
            type: "int",
            nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "Trip",
                type: "datetime",
                nullable: true);
            migrationBuilder.AddColumn<DateTime>(
                name: "endTime",
                table: "Trip",
                type: "datetime",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "startLocation",
                table: "Trip",
                 maxLength: 255, // Set the maximum length as needed
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "endLocation",
                table: "Trip",
                 maxLength: 255, // Set the maximum length as needed
                nullable: true);

            migrationBuilder.AddForeignKey(
            name: "FK_Trip_cab_cabId",
            table: "Trip",
            column: "cabId",
            principalTable: "cab",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
