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
                    ID_MARCA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.IDMODELO);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_ID_MARCA",
                        column: x => x.ID_MARCA,
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
                    idModelo = table.Column<int>(type: "int", nullable: true),
                    ModeloIDMODELO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.IDVEHICULO);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Modelos_ModeloIDMODELO",
                        column: x => x.ModeloIDMODELO,
                        principalTable: "Modelos",
                        principalColumn: "IDMODELO");
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "IDMARCA", "NOM_MARCA" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Honda" },
                    { 3, "Ford" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "IDVEHICULO", "Color", "ModeloIDMODELO", "NRO_PLACA", "año", "estado", "idModelo" },
                values: new object[,]
                {
                    { 1, "Morado", null, "ABC123", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1 },
                    { 2, "Azul", null, "XYZ789", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2 },
                    { 3, "Rojo", null, "LMN456", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3 },
                    { 4, "Blanco", null, "QRS852", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4 },
                    { 5, "Negro", null, "GHI789", new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5 },
                    { 6, "Verde", null, "JKL321", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6 }
                });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "IDMODELO", "ID_MARCA", "NOM_MODELO" },
                values: new object[,]
                {
                    { 1, 1, "Corolla" },
                    { 2, 1, "Camry" },
                    { 3, 2, "Civic" },
                    { 4, 2, "Accord" },
                    { 5, 3, "Focus" },
                    { 6, 3, "Mustang" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_ID_MARCA",
                table: "Modelos",
                column: "ID_MARCA");

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
