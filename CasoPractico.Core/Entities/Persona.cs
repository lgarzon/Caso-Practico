using System.ComponentModel.DataAnnotations;

namespace CasoPractico.Core.Entities
{
    public class Persona
    {
        [Required]
        [MaxLength(150)]
        public string nombre { get; set; }

        [Required]
        [MaxLength(20)]
        public string genero { get; set; }

        [Required]
        public int edad { get; set; }

        [Required]
        [MaxLength(20)]
        public string identificacion { get; set; }

        [Required]
        [MaxLength(200)]
        public string direccion { get; set; }

        [Required]
        [MaxLength(20)]
        public string telefono { get; set; }
    }
}
