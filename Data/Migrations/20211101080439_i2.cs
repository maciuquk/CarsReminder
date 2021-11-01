using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsReminder.Data.Migrations
{
    public partial class i2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "ItemsToRemind",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "ItemsToRemind");
        }
    }
}
