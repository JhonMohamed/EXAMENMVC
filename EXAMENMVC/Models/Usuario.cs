using System.ComponentModel.DataAnnotations;

namespace EXAMENMVC.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}