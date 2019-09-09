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
    [Migration("20190909131103_fotos")]
    partial class fotos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("IdentityUserClaim");
                });

            modelBuilder.Entity("RegiVeSec.Models.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detalles");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

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

            modelBuilder.Entity("RegiVeSec.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detalles");

                    b.HasKey("Id");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("RegiVeSec.Models.VehiculoRegiVeSec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Causa")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("DependenciaProcedente")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Deposito")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Dominio")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Entrega")
                        .HasMaxLength(30);

                    b.Property<int>("EstadoId");

                    b.Property<DateTime>("FechaDeEntrega");

                    b.Property<DateTime>("FechaDeIngreso");

                    b.Property<int?>("LoginId");

                    b.Property<string>("MagistradoInterviniente")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("NumeroSumario")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Orden")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Propietario")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Recibe")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("SumarioRegistrar")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("TipoId");

                    b.Property<string>("UbicacionActual")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("foto");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("LoginId");

                    b.HasIndex("TipoId");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("RegiVeSec.Models.VehiculoRegiVeSec", b =>
                {
                    b.HasOne("RegiVeSec.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RegiVeSec.Models.Login")
                        .WithMany("vehiculoRegiVeSecs")
                        .HasForeignKey("LoginId");

                    b.HasOne("RegiVeSec.Models.Tipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
