using CasoPractico.Core.Entities;
using CasoPractico.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasoPractico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _service;

        public CuentasController(ICuentaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{numeroCuenta}")]
        public async Task<IActionResult> Get(long numeroCuenta)
        {
            return Ok(await _service.Get(numeroCuenta));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (int resultado, string mensaje) = await _service.AddCuenta(cuenta);

            if (resultado == 1)
            {
                return Ok(mensaje);
            }
            return BadRequest(mensaje);
        }

        [HttpDelete("{numeroCuenta}")]
        public async Task<IActionResult> Delete(long numeroCuenta)
        {
            var existingCuenta = await _service.Get(numeroCuenta);
            if (existingCuenta is not null)
            {
                (int resultado, string mensaje) = await _service.DeleteCuenta(existingCuenta.numeroCuenta);

                if (resultado == 1)
                {
                    return Ok(mensaje);
                }
                return BadRequest(mensaje);
            }
            return NotFound($"No encontrada la cuenta : {numeroCuenta}");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Cuenta cuenta)
        {
            var existingCuenta = await _service.Get(cuenta.numeroCuenta);
            if (existingCuenta is not null)
            {
                (int resultado, string mensaje)  = await _service.UpdateCuenta(cuenta);
                if (resultado == 1)
                {
                    return Ok(mensaje);
                }
                return BadRequest(mensaje);
            }
            return NotFound($"No encontrada la cuenta : {cuenta.numeroCuenta}");
        }
    }
}
