using System.ComponentModel.DataAnnotations;

namespace EXAMENMVC.Models
{
    public class Modelo
    {
        [Key]
        public int IDMODELO { get; set; }
        
        public string NOM_MODELO { get; set; }

        public int MarcaIDMARCA { get; set; }

        // Propiedad de navegación hacia la clase Marca
        public Marca Marca { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
