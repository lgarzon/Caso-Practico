using CasoPractico.Core.Entities;
using CasoPractico.Core.Services;
using CasoPractico.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasoPractico.Application.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ContextDatabase _contextDatabase;
        private readonly ILogger _logger;

        public CuentaService(ContextDatabase contextDatabase, ILogger<CuentaService> logger)
        {
            _contextDatabase = contextDatabase;
            _logger = logger;
        }

        public async Task<IEnumerable<Cuenta>> GetAll()
        {
            try
            {
                return await _contextDatabase.Cuentas.ToListAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al obtener las cuentas, {ex}");
                return null;
            }
        }

        public async Task<Cuenta> Get(long numeroCuenta)
        {
            try
            {
                return await _contextDatabase.Cuentas.FirstOrDefaultAsync(c => c.numeroCuenta == numeroCuenta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al obtener la cuenta, {ex}");
                return null;
            }
        }

        public async Task<Cuenta> AddCuenta(Cuenta cuenta)
        {
            try
            {
                await _contextDatabase.Cuentas.AddAsync(cuenta);
                _contextDatabase.SaveChanges();
                return cuenta;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al guardar la cuenta, {ex}");
                return null;
            }
        }

        public async Task DeleteCuenta(long id)
        {
            try
            {
                var cuenta = await _contextDatabase.Cuentas.FirstOrDefaultAsync(c => c.numeroCuenta == id);

                if (cuenta is not null)
                {
                    _contextDatabase.Cuentas.Remove(cuenta);
                    _contextDatabase.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al eliminar la cuenta, {ex}");
            }
        }

        public void UpdateCuenta(Cuenta cuenta)
        {
            try
            {
                _contextDatabase.Cuentas.Update(cuenta);
                _contextDatabase.SaveChanges();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al actualizar la cuenta, {ex}");
            }
        }
    }
}
