using System.ComponentModel.DataAnnotations;

namespace EXAMENMVC.Models
{
    public class VehiculoVM
    {
        [Key]
     


        public string NRO_PLACA { get; set; }
        public string NOM_MARCA { get; set; }
        public string NOM_MODELO { get; set; }


        public DateTime año { get; set; }


        public bool estado { get; set; }


        public string Color { get; set; }


        public int ModeloIDMODELO { get; set; }


        public IFormFile ImagenFile { get; set; }

        public int MarcaIDMARCA { get; set; }
    }
}