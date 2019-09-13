using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegiVeSec.Migrations
{
    public partial class listaDeFotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImagenPorVehuculoid",
                table: "Vehiculos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImagenPorVehuculo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdVehiculos = table.Column<int>(nullable: false),
                    DirecccionDeFotos = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenPorVehuculo", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ImagenPorVehuculoid",
                table: "Vehiculos",
                column: "ImagenPorVehuculoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_ImagenPorVehuculo_ImagenPorVehuculoid",
                table: "Vehiculos",
                column: "ImagenPorVehuculoid",
                principalTable: "ImagenPorVehuculo",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_ImagenPorVehuculo_ImagenPorVehuculoid",
                table: "Vehiculos");

            migrationBuilder.DropTable(
                name: "ImagenPorVehuculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_ImagenPorVehuculoid",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "ImagenPorVehuculoid",
                table: "Vehiculos");
        }
    }
}
