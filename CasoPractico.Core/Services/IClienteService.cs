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

        Task<(int, string)> AddCliente(Cliente cliente);

        Task<(int, string)> DeleteCliente(long id);

        Task<(int, string)> UpdateCliente(Cliente cliente);
    }
}
