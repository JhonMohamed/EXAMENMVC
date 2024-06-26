﻿using System.ComponentModel.DataAnnotations;

namespace EXAMENMVC.Models
{
    public class Vehiculo
    {
        [Key]
        public int IDVEHICULO { get; set; }
        public string NRO_PLACA { get; set; }
        public string NOM_MARCA {  get; set; }
        public string NOM_MODELO { get; set; }
        public DateTime año { get; set; }
        public bool estado { get; set; }

        public string Color { get; set; }
        [MaxLength(50)]
        public string? Imagen { get; set; }

        // Cambia el nombre de la propiedad a ModeloIDMODELO
        public int ModeloIDMODELO { get; set; }
        // Propiedad de navegación hacia la clase Modelo
        public Modelo? Modelo { get; set; }
    }
}