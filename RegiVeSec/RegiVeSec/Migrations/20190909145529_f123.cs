using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegiVeSec.Migrations
{
    public partial class f123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ImagenPorVehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehiculoId = table.Column<int>(nullable: true),
                    DirecccionDeFoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenPorVehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenPorVehiculo_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagenPorVehiculo_VehiculoId",
                table: "ImagenPorVehiculo",
                column: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagenPorVehiculo");

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
                    DirecccionDeFotos = table.Column<string>(nullable: true),
                    IdVehiculos = table.Column<int>(nullable: false)
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
    }
}
