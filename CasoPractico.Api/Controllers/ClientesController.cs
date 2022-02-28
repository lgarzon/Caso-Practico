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
            await _service.AddCliente(cliente);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existingCliente = await _service.Get(id);
            if (existingCliente is not null)
            {
                await _service.DeleteCliente(existingCliente.clienteId);
                return Ok();
            }
            return NotFound($"Cliente no encontrado con el id : {id}");
        }

        [HttpPut]
        public IActionResult Update(Cliente cliente)
        {
            _service.UpdateCliente(cliente);
            return Ok();
        }
    }
}
