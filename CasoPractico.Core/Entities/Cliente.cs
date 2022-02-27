using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CasoPractico.Core.Entities
{
    public class Cliente : Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long clienteId { get; set; }

        [Required]
        [MaxLength(100)]
        public string contrasenia { get; set; }

        [Required]
        public bool estado { get; set; }
    }
}
