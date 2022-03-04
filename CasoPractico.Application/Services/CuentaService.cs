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
                return await _contextDatabase.Cuentas.AsNoTracking().FirstOrDefaultAsync(c => c.numeroCuenta == numeroCuenta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al obtener la cuenta, {ex}");
                return null;
            }
        }

        public async Task<(int, string)> AddCuenta(Cuenta cuenta)
        {
            int result;

            try
            {
                _contextDatabase.Cuentas.Add(cuenta);
                await _contextDatabase.SaveChangesAsync();

                result = 1;
                return (result, "Cuenta guardada correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al guardar la cuenta, {ex}");
                result = 0;
                return (result, "Error al guardar cuenta");
            }
        }

        public async Task<(int, string)> DeleteCuenta(long id)
        {
            int result;

            try
            {
                var cuenta = await _contextDatabase.Cuentas.AsNoTracking().FirstOrDefaultAsync(c => c.numeroCuenta == id);

                if (cuenta is not null)
                {
                    _contextDatabase.Cuentas.Remove(cuenta);
                    await _contextDatabase.SaveChangesAsync();

                    result = 1;
                    return (result, "Cuenta eliminada correctamente");
                }

                result = 0;
                return (result, "Cuenta no encontrada.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al eliminar la cuenta, {ex}");
                result = 0;
                return (result, "Error al eliminar cuenta");
            }
        }

        public async Task<(int, string)> UpdateCuenta(Cuenta cuenta)
        {
            int result;

            try
            {
                _contextDatabase.Cuentas.Update(cuenta);
                await _contextDatabase.SaveChangesAsync();

                result = 1;
                return (result, "Cuenta actualizada correctamente");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al actualizar la cuenta, {ex}");
                result = 0;
                return (result, "Error al actualizar cuenta");
            }
        }
    }
}
