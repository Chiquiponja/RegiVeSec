using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegiVeSec.Migrations
{
    public partial class Seccion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loginId",
                table: "Vehiculos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Contrasenia = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_loginId",
                table: "Vehiculos",
                column: "loginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Logins_loginId",
                table: "Vehiculos",
                column: "loginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Logins_loginId",
                table: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_loginId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "loginId",
                table: "Vehiculos");
        }
    }
}
