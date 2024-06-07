﻿// <auto-generated />
using System;
using EXAMENMVC.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EXAMENMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EXAMENMVC.Models.Marca", b =>
                {
                    b.Property<int>("IDMARCA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDMARCA"), 1L, 1);

                    b.Property<string>("NOM_MARCA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDMARCA");

                    b.ToTable("Marcas");

                    b.HasData(
                        new
                        {
                            IDMARCA = 1,
                            NOM_MARCA = "Toyota"
                        },
                        new
                        {
                            IDMARCA = 2,
                            NOM_MARCA = "Honda"
                        },
                        new
                        {
                            IDMARCA = 3,
                            NOM_MARCA = "Ford"
                        });
                });

            modelBuilder.Entity("EXAMENMVC.Models.Modelo", b =>
                {
                    b.Property<int>("IDMODELO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDMODELO"), 1L, 1);

                    b.Property<int>("ID_MARCA")
                        .HasColumnType("int");

                    b.Property<string>("NOM_MODELO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDMODELO");

                    b.HasIndex("ID_MARCA");

                    b.ToTable("Modelos");

                    b.HasData(
                        new
                        {
                            IDMODELO = 1,
                            ID_MARCA = 1,
                            NOM_MODELO = "Corolla"
                        },
                        new
                        {
                            IDMODELO = 2,
                            ID_MARCA = 1,
                            NOM_MODELO = "Camry"
                        },
                        new
                        {
                            IDMODELO = 3,
                            ID_MARCA = 2,
                            NOM_MODELO = "Civic"
                        },
                        new
                        {
                            IDMODELO = 4,
                            ID_MARCA = 2,
                            NOM_MODELO = "Accord"
                        },
                        new
                        {
                            IDMODELO = 5,
                            ID_MARCA = 3,
                            NOM_MODELO = "Focus"
                        },
                        new
                        {
                            IDMODELO = 6,
                            ID_MARCA = 3,
                            NOM_MODELO = "Mustang"
                        });
                });

            modelBuilder.Entity("EXAMENMVC.Models.Vehiculo", b =>
                {
                    b.Property<int>("IDVEHICULO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDVEHICULO"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModeloIDMODELO")
                        .HasColumnType("int");

                    b.Property<string>("NRO_PLACA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("año")
                        .HasColumnType("datetime2");

                    b.Property<bool>("estado")
                        .HasColumnType("bit");

                    b.Property<int?>("idModelo")
                        .HasColumnType("int");

                    b.HasKey("IDVEHICULO");

                    b.HasIndex("ModeloIDMODELO");

                    b.ToTable("Vehiculos");

                    b.HasData(
                        new
                        {
                            IDVEHICULO = 1,
                            Color = "Morado",
                            NRO_PLACA = "ABC123",
                            año = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            estado = true,
                            idModelo = 1
                        },
                        new
                        {
                            IDVEHICULO = 2,
                            Color = "Azul",
                            NRO_PLACA = "XYZ789",
                            año = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            estado = true,
                            idModelo = 2
                        },
                        new
                        {
                            IDVEHICULO = 3,
                            Color = "Rojo",
                            NRO_PLACA = "LMN456",
                            año = new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            estado = true,
                            idModelo = 3
                        },
                        new
                        {
                            IDVEHICULO = 4,
                            Color = "Blanco",
                            NRO_PLACA = "QRS852",
                            año = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            estado = true,
                            idModelo = 4
                        },
                        new
                        {
                            IDVEHICULO = 5,
                            Color = "Negro",
                            NRO_PLACA = "GHI789",
                            año = new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            estado = true,
                            idModelo = 5
                        },
                        new
                        {
                            IDVEHICULO = 6,
                            Color = "Verde",
                            NRO_PLACA = "JKL321",
                            año = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            estado = true,
                            idModelo = 6
                        });
                });

            modelBuilder.Entity("EXAMENMVC.Models.Modelo", b =>
                {
                    b.HasOne("EXAMENMVC.Models.Marca", "Marca")
                        .WithMany("Modelos")
                        .HasForeignKey("ID_MARCA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Vehiculo", b =>
                {
                    b.HasOne("EXAMENMVC.Models.Modelo", "Modelo")
                        .WithMany("Vehiculos")
                        .HasForeignKey("ModeloIDMODELO");

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Marca", b =>
                {
                    b.Navigation("Modelos");
                });

            modelBuilder.Entity("EXAMENMVC.Models.Modelo", b =>
                {
                    b.Navigation("Vehiculos");
                });
#pragma warning restore 612, 618
        }
    }
}