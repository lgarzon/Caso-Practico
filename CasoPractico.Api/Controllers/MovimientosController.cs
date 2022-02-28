using CasoPractico.Core.DTOs;
using CasoPractico.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasoPractico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService _service;

        public MovimientosController(IMovimientoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovimientoDto movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (int resultado, string mensaje) = await _service.AddMovimiento(movimiento);

            if (resultado == 1)
            {
                return Ok(mensaje);
            }
            return BadRequest(mensaje);
        }

        [HttpGet]
        public IActionResult EstadoCuenta([FromQuery] EstadoCuentaDto estadoCuentaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.GenerateEstadoCuenta(estadoCuentaDto));
        }
    }
}
