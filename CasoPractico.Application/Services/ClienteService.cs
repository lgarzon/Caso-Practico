using CasoPractico.Core.Entities;
using CasoPractico.Core.Services;
using CasoPractico.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPractico.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ContextDatabase _contextDatabase;
        private readonly ILogger _logger;

        public ClienteService(ContextDatabase contextDatabase, ILogger<ClienteService> logger)
        {
            _contextDatabase = contextDatabase;
            _logger = logger;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            try
            {
                return await _contextDatabase.Clientes.ToListAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al obtener los clientes, {ex}");
                return null;
            }
        }

        public async Task<Cliente> Get(long id)
        {
            try
            {
                return await _contextDatabase.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.clienteId == id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al obtener el cliente, {ex}");
                return null;
            }
            
        }

        public async Task<(int, string)> AddCliente(Cliente cliente)
        {
            int result;

            try
            {
                _contextDatabase.Clientes.Add(cliente);
                await _contextDatabase.SaveChangesAsync();

                result = 1;
                return (result, "Cliente guardado correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al guardar el cliente, {ex}");
                result = 0;
                return (result, "Error al guardar cliente");
            }
        }

        public async Task<(int, string)> DeleteCliente(long id)
        {
            int result;

            try
            {
                var cliente = await _contextDatabase.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.clienteId == id);

                if (cliente is not null)
                {
                    _contextDatabase.Clientes.Remove(cliente);
                    await _contextDatabase.SaveChangesAsync();

                    result = 1;
                    return (result, "Cliente eliminado correctamente");
                }

                result = 0;
                return (result, "Cliente no encontrado.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al eliminar el cliente, {ex}");
                result = 0;
                return (result, "Error al eliminar cliente");
            }
        }

        public async Task<(int, string)> UpdateCliente(Cliente cliente)
        {
            int result;

            try
            {
                _contextDatabase.Clientes.Update(cliente);
                await _contextDatabase.SaveChangesAsync();

                result = 1;
                return (result, "Cliente actualizado correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al actualizar el cliente, {ex}");
                result = 0;
                return (result, "Error al modificar cliente");
            }
        }
    }
}
