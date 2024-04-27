using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterProService.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name:"User_Detail",newName:"user_detail");
            migrationBuilder.RenameTable(name: "Cab", newName: "cab");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
