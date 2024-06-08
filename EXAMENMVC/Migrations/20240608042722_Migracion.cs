using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXAMENMVC.Migrations
{
    public partial class Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    IDMARCA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_MARCA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.IDMARCA);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    IDMODELO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_MODELO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaIDMARCA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.IDMODELO);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaIDMARCA",
                        column: x => x.MarcaIDMARCA,
                        principalTable: "Marcas",
                        principalColumn: "IDMARCA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    IDVEHICULO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NRO_PLACA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    año = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloIDMODELO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.IDVEHICULO);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Modelos_ModeloIDMODELO",
                        column: x => x.ModeloIDMODELO,
                        principalTable: "Modelos",
                        principalColumn: "IDMODELO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 1, "Toyota" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 2, "Honda" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[] { 3, "Ford" });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "IDMODELO", "MarcaIDMARCA", "NOM_MODELO" },
                values: new object[,]
                {
                    { 1, 1, "Corolla" },
                    { 2, 1, "Camry" },
                    { 3, 2, "Civic" },
                    { 4, 2, "Accord" },
                    { 5, 3, "Focus" },
                    { 6, 3, "Mustang" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "IDVEHICULO", "Color", "ModeloIDMODELO", "NRO_PLACA", "año", "estado" },
                values: new object[,]
                {
                    { 1, "Morado", 1, "ABC123", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, "Azul", 2, "XYZ789", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 3, "Rojo", 3, "LMN456", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 4, "Blanco", 4, "QRS852", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5, "Negro", 5, "GHI789", new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 6, "Verde", 6, "JKL321", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaIDMARCA",
                table: "Modelos",
                column: "MarcaIDMARCA");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ModeloIDMODELO",
                table: "Vehiculos",
                column: "ModeloIDMODELO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
