using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [ForeignKey("Cliente")]
        public int clienteId { get; set; }

        public Cliente cliente { get; set; }
    }
}
