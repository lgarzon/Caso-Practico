using CasoPractico.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPractico.Core.Services
{
    public interface ICuentaService
    {
        Task<IEnumerable<Cuenta>> GetAll();

        Task<Cuenta> Get(long numeroCuenta);

        Task<(int, string)> AddCuenta(Cuenta cuenta);

        Task<(int, string)> DeleteCuenta(long id);

        Task<(int, string)> UpdateCuenta(Cuenta cuenta);
    }
}
