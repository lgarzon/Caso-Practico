using CasoPractico.Core.Entities;
using System.Collections.Generic;

namespace CasoPractico.Core.DTOs
{
    public class CuentaDto
    {
        public long numeroCuenta { get; set; }

        public string tipoCuenta { get; set; }

        public decimal saldoInicial { get; set; }

        public bool estado { get; set; }

        public IEnumerable<Movimiento> movimientos { get; set; }
    }
}
