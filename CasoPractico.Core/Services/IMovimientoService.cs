using CasoPractico.Core.DTOs;
using CasoPractico.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPractico.Core.Services
{
    public interface IMovimientoService
    {
        Task<(int, string)> AddMovimiento(MovimientoDto movimiento);
        
        IEnumerable<ReporteEstadoCuentaDto> GenerateEstadoCuenta(EstadoCuentaDto dto);
    }
}
