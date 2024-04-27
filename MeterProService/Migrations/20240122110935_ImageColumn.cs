using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class ImageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Trip",  
                type: "VARBINARY(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
