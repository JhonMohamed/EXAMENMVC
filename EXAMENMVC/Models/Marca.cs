using System.ComponentModel.DataAnnotations;

namespace EXAMENMVC.Models
{
    public class Marca
    {
        [Key]
        public int IDMARCA { get; set; }
        public string NOM_MARCA { get; set; }
        public ICollection<Modelo> Modelos { get; set; }
    }
}