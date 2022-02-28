using System.Collections.Generic;

namespace CasoPractico.Core.DTOs
{
    public class ReporteEstadoCuentaDto
    {
        public long clienteId { get; set; }

        public string identificacion { get; set; }

        public string nombre { get; set; }

        public IEnumerable<CuentaDto> cuentas { get; set; }
    }
}
