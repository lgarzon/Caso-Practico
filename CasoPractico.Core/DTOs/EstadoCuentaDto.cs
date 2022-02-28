using System;

namespace CasoPractico.Core.DTOs
{
    public class EstadoCuentaDto
    {
        public DateTime fechaInicial { get; set; }

        public DateTime fechaFinal { get; set; }

        public long clienteId { get; set; }
    }
}
