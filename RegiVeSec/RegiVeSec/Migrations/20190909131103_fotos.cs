using Microsoft.EntityFrameworkCore.Migrations;

namespace RegiVeSec.Migrations
{
    public partial class fotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "foto",
                table: "Vehiculos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foto",
                table: "Vehiculos");
        }
    }
}
