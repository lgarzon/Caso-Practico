using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CasoPractico.Core.Entities
{
    public class Movimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idMovimiento { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        [Required]
        [MaxLength(20)]
        public string tipoMovimiento { get; set; }

        [Required]
        [Column(TypeName = "decimal(14, 2)")]
        public decimal valor { get; set; }

        [Required]
        [Column(TypeName = "decimal(14, 2)")]
        public decimal saldoDisponible { get; set; }

        public long numeroCuenta { get; set; }

        [JsonIgnore]
        public Cuenta cuenta { get; set; }
    }
}
