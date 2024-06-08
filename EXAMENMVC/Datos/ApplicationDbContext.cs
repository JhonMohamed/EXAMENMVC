using Microsoft.EntityFrameworkCore;
using EXAMENMVC.Models;

namespace EXAMENMVC.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modelo>()
                .HasOne(p => p.Marca)
                .WithMany(b => b.Modelos)
                .HasForeignKey(p => p.MarcaIDMARCA);

            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Modelo)
                .WithMany(m => m.Vehiculos)
                .HasForeignKey(v => v.ModeloIDMODELO);

            // Datos semilla
            modelBuilder.Entity<Marca>().HasData(
                new Marca { IDMARCA = 1, NOM_MARCA = "Toyota" },
                new Marca { IDMARCA = 2, NOM_MARCA = "Honda" },
                new Marca { IDMARCA = 3, NOM_MARCA = "Ford" }
            );

            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { IDMODELO = 1, NOM_MODELO = "Corolla", MarcaIDMARCA = 1 },
                new Modelo { IDMODELO = 2, NOM_MODELO = "Camry", MarcaIDMARCA = 1 },
                new Modelo { IDMODELO = 3, NOM_MODELO = "Civic", MarcaIDMARCA = 2 },
                new Modelo { IDMODELO = 4, NOM_MODELO = "Accord", MarcaIDMARCA = 2 },
                new Modelo { IDMODELO = 5, NOM_MODELO = "Focus", MarcaIDMARCA = 3 },
                new Modelo { IDMODELO = 6, NOM_MODELO = "Mustang", MarcaIDMARCA = 3 }
            );

            modelBuilder.Entity<Vehiculo>().HasData(
                new Vehiculo { IDVEHICULO = 1, NRO_PLACA = "ABC123", año = new DateTime(2020, 1, 1), estado = true, Color = "Morado", ModeloIDMODELO = 1 },
                new Vehiculo { IDVEHICULO = 2, NRO_PLACA = "XYZ789", año = new DateTime(2019, 1, 1), estado = true, Color = "Azul", ModeloIDMODELO = 2 },
                new Vehiculo { IDVEHICULO = 3, NRO_PLACA = "LMN456", año = new DateTime(2018, 1, 1), estado = true, Color = "Rojo", ModeloIDMODELO = 3 },
                new Vehiculo { IDVEHICULO = 4, NRO_PLACA = "QRS852", año = new DateTime(2021, 1, 1), estado = true, Color = "Blanco", ModeloIDMODELO = 4 },
                new Vehiculo { IDVEHICULO = 5, NRO_PLACA = "GHI789", año = new DateTime(2017, 1, 1), estado = true, Color = "Negro", ModeloIDMODELO = 5 },
                new Vehiculo { IDVEHICULO = 6, NRO_PLACA = "JKL321", año = new DateTime(2022, 1, 1), estado = true, Color = "Verde", ModeloIDMODELO = 6 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}