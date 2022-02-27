using CasoPractico.Core.Entities;
using CasoPractico.Core.Services;
using CasoPractico.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPractico.Application.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ContextDatabase _contextDatabase;

        public CuentaService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<IEnumerable<Cuenta>> GetAll()
        {
            return await _contextDatabase.Cuentas.ToListAsync();
        }

        public async Task<Cuenta> Get(long numeroCuenta)
        {
            return await _contextDatabase.Cuentas.FirstOrDefaultAsync(c => c.numeroCuenta == numeroCuenta);
        }
    }
}
