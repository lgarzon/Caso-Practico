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
    }
}
