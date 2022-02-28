using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CasoPractico.Core.Entities
{
    public class CupoDiario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        [Required]
        [Column(TypeName = "decimal(14, 2)")]
        public decimal cupo { get; set; }
    }
}
