using CasoPractico.Core.Entities;
using CasoPractico.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasoPractico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (int resultado, string mensaje) = await _service.AddCliente(cliente);

            if (resultado == 1)
            {
                return Ok(mensaje);
            }
            return BadRequest(mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existingCliente = await _service.Get(id);
            if (existingCliente is not null)
            {
                (int resultado, string mensaje) = await _service.DeleteCliente(existingCliente.clienteId);

                if (resultado == 1)
                {
                    return Ok(mensaje);
                }
                return BadRequest(mensaje);
            }
            return NotFound($"Cliente no encontrado con el id : {id}");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Cliente cliente)
        {
            var existingCliente = await _service.Get(cliente.clienteId);
            if (existingCliente is not null)
            {
                (int resultado, string mensaje) = await _service.UpdateCliente(cliente);
                if (resultado == 1)
                {
                    return Ok(mensaje);
                }
                return BadRequest(mensaje);
            }
            return NotFound($"Cliente no encontrado con el id : {cliente.clienteId}");
          
        }
    }
}
