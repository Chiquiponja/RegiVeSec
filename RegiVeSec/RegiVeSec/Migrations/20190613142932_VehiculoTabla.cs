using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegiVeSec.Migrations
{
    public partial class VehiculoTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaDeIngreso = table.Column<DateTime>(nullable: false),
                    Propietario = table.Column<string>(nullable: true),
                    Dominio = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Causa = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    NumeroSumario = table.Column<string>(nullable: true),
                    Dependencia = table.Column<string>(nullable: true),
                    Orden = table.Column<string>(nullable: true),
                    DependenciaProcedente = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    Recibe = table.Column<string>(nullable: true),
                    Entrega = table.Column<string>(nullable: true),
                    FechaDeEntrega = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
