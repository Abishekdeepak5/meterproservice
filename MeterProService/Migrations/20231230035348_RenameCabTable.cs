using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class RenameCabTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Cab", newName: "cab");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
