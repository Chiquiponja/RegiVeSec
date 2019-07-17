﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegiVeSec.Data;

namespace RegiVeSec.Migrations
{
    [DbContext(typeof(Conexionbd))]
    [Migration("20190712133820_A")]
    partial class A
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RegiVeSec.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contrasenia");

                    b.Property<bool>("Estado");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("RegiVeSec.Models.VehiculoRegiVeSec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Causa");

                    b.Property<string>("Color");

                    b.Property<string>("Dependencia");

                    b.Property<string>("DependenciaProcedente");

                    b.Property<string>("Dominio");

                    b.Property<string>("Entrega");

                    b.Property<string>("Estado");

                    b.Property<DateTime>("FechaDeEntrega");

                    b.Property<DateTime>("FechaDeIngreso");

                    b.Property<string>("Marca");

                    b.Property<string>("Modelo");

                    b.Property<string>("NumeroSumario");

                    b.Property<string>("Observaciones");

                    b.Property<string>("Orden");

                    b.Property<string>("Propietario");

                    b.Property<string>("Recibe");

                    b.Property<string>("Tipo");

                    b.Property<int?>("loginId");

                    b.HasKey("Id");

                    b.HasIndex("loginId");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("RegiVeSec.Models.VehiculoRegiVeSec", b =>
                {
                    b.HasOne("RegiVeSec.Models.Login", "login")
                        .WithMany("vehiculoRegiVeSecs")
                        .HasForeignKey("loginId");
                });
#pragma warning restore 612, 618
        }
    }
}