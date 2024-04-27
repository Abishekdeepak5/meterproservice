using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class updateTripTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<String>(
                name: "address",
                table: "Trip",
                nullable: true);
            //migrationBuilder.AlterColumn<String>(
            //    name: "address",
            //    table: "Trip",
            //    nullable: true);

            migrationBuilder.AlterColumn<float>(
            name: "price",
            table: "Trip", // Replace with your actual table name
            type: "FLOAT(53)",
            nullable: true // Set to true to allow null
             );

            migrationBuilder.AlterColumn<decimal>(
                name: "latitude",
                table: "Trip", // Replace with your actual table name
                //type: "DECIMAL(12, 9)",
                nullable: true // Set to true to allow null
                );

            migrationBuilder.AlterColumn<decimal>(
                name: "longitude",
                table: "Trip", // Replace with your actual table name
               nullable: true);
            //migrationBuilder.AlterColumn<decimal>(
            //  name: "cabId",
            //  table: "Trip", // Replace with your actual table name
            // nullable: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
