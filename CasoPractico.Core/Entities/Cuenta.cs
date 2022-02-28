using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CasoPractico.Core.Entities
{
    public class Cuenta
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long numeroCuenta { get; set; }

        [Required]
        [MaxLength(20)]
        public string tipoCuenta { get; set; }

        [Required]
        [Column(TypeName = "decimal(14, 2)")]
        public decimal saldoInicial { get; set; }

        [Required]
        public bool estado { get; set; }

        public long clienteId { get; set; }

        [JsonIgnore]
        public Cliente cliente { get; set; }

        [JsonIgnore]
        public ICollection<Movimiento> movimientos { get; set; }
    }
}
