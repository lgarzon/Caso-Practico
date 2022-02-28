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

            await _service.AddCuenta(cuenta);
            return Ok();
        }

        [HttpDelete("{numeroCuenta}")]
        public async Task<IActionResult> Delete(long numeroCuenta)
        {
            var existingCuenta = await _service.Get(numeroCuenta);
            if (existingCuenta is not null)
            {
                await _service.DeleteCuenta(existingCuenta.numeroCuenta);
                return Ok();
            }
            return NotFound($"Cuenta no encontrado con el id : {numeroCuenta}");
        }

        [HttpPut]
        public IActionResult Update(Cuenta cuenta)
        {
            _service.UpdateCuenta(cuenta);
            return Ok();
        }
    }
}
