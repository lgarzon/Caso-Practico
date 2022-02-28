using CasoPractico.Core.DTOs;
using CasoPractico.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPractico.Core.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAll();
        
        Task<Cliente> Get(long id);

        Task<Cliente> AddCliente(Cliente cliente);

        Task DeleteCliente(long id);

        Task UpdateCliente(Cliente cliente);
    }
}
