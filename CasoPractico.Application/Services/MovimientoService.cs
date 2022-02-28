using CasoPractico.Core.DTOs;
using CasoPractico.Core.Entities;
using CasoPractico.Core.Services;
using CasoPractico.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasoPractico.Application.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly ContextDatabase _contextDatabase;
        private readonly ILogger _logger;

        public MovimientoService(ContextDatabase contextDatabase, ILogger<MovimientoService> logger)
        {
            _contextDatabase = contextDatabase;
            _logger = logger;
        }

        public async Task<(int, string)> AddMovimiento(MovimientoDto movimiento)
        {
            (int result, string mensaje) = (0, "");
            try
            {
                //Validar cuenta
                var cuenta = await _contextDatabase.Cuentas.FirstOrDefaultAsync(c => c.numeroCuenta == movimiento.numeroCuenta);

                if (cuenta is null)
                {
                    return (result, $"La cuenta {movimiento.numeroCuenta} no existe.");
                }

                //Deposito
                if (movimiento.tipoMovimiento.Equals("Deposito"))
                {
                    (result, mensaje) = await RealizarDeposito(movimiento, cuenta);
                    
                }

                //Retiro
                if (movimiento.tipoMovimiento.Equals("Retiro"))
                {
                    (result, mensaje) = await RealizarRetiro(movimiento, cuenta);
                }

                return (result, mensaje);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al realizar el movimiento, {ex}");
                return (result, "Error interno al realizar movimiento.");
            }
        }

        public IEnumerable<ReporteEstadoCuentaDto> GenerateEstadoCuenta(EstadoCuentaDto dto)
        {
            try
            {
                var cliente = _contextDatabase.Clientes
                    .Select(c => new ReporteEstadoCuentaDto
                    {
                        clienteId = c.clienteId,
                        identificacion = c.identificacion,
                        nombre = c.nombre,
                        cuentas = c.cuentas.Select(ct => new CuentaDto 
                        {
                            numeroCuenta = ct.numeroCuenta,
                            tipoCuenta = ct.tipoCuenta,
                            saldoInicial = ct.saldoInicial,
                            estado = ct.estado,
                            movimientos = ct.movimientos.Select(m => new Movimiento
                            {
                                idMovimiento = m.idMovimiento,
                                numeroCuenta = m.numeroCuenta,
                                tipoMovimiento = m.tipoMovimiento,
                                fecha = m.fecha,
                                valor = m.valor,
                                saldoDisponible = m.saldoDisponible
                            }).Where(m => m.fecha.Date >= dto.fechaInicial && m.fecha.Date <= dto.fechaFinal).ToList()
                        }).ToList()
                    }).Where(c => c.clienteId == dto.clienteId).ToList();

                return cliente;
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al generar estado de cuenta, {ex}");
                return null;
            }
        }

        private async Task<(int, string)> RealizarDeposito(MovimientoDto movimientoDto, Cuenta cuenta)
        {
            int result = 0;

            try
            {
                if (movimientoDto.valor <= 0)
                {
                    return (result, $"El valor para un depósito debe ser mayor a cero. Valor ingresado {movimientoDto.valor}");
                }

                decimal saldoActual = cuenta.saldoInicial + movimientoDto.valor;

                //Creamos movimiento
                Movimiento movimiento = new Movimiento()
                {
                    numeroCuenta = cuenta.numeroCuenta,
                    fecha = DateTime.Now,
                    tipoMovimiento = movimientoDto.tipoMovimiento,
                    valor = movimientoDto.valor,
                    saldoDisponible = saldoActual
                };

                _contextDatabase.Movimientos.Add(movimiento);

                //Actualizamos saldoInicial de la cuenta
                cuenta.saldoInicial = saldoActual;
                _contextDatabase.Cuentas.Update(cuenta);

                //Guardamos
                await _contextDatabase.SaveChangesAsync();

                result = 1;
                return (result, "Depósito realizado corretamente");
            }
            catch (Exception ex)
            {
                result = 0;
                _logger.LogError($"Error al realizar el depósito, {ex}");
                return (result, "Error interno al realizar movimiento.");
            }
        }

        private async Task<(int, string)> RealizarRetiro(MovimientoDto movimientoDto, Cuenta cuenta)
        {
            int result = 0;

            try
            {
                if (movimientoDto.valor >= 0)
                {
                    return (result, $"El valor para un retiro debe ser menor a cero. Valor ingresado {movimientoDto.valor}");
                }

                decimal saldoActual = cuenta.saldoInicial + movimientoDto.valor;

                //Validamos saldo
                if (saldoActual < 0)
                {
                    return (result, "Saldo no disponible");
                }

                //validar cupo diario
                var cupoDiario = _contextDatabase.CuposDiarios.FirstOrDefault(c => c.fecha == DateTime.Now.Date);

                if (cupoDiario is null)
                {
                    return (result, $"Error al recuperar el cupo diario");
                }

                decimal cupo = cupoDiario.cupo + movimientoDto.valor;

                if (cupo < 0)
                {
                    return (result, $"Cupo diario Excedido");
                }

                cupoDiario.cupo = cupo;
                _contextDatabase.CuposDiarios.Update(cupoDiario);

                //Creamos movimiento
                Movimiento movimiento = new Movimiento()
                {
                    numeroCuenta = cuenta.numeroCuenta,
                    fecha = DateTime.Now,
                    tipoMovimiento = movimientoDto.tipoMovimiento,
                    valor = movimientoDto.valor,
                    saldoDisponible = saldoActual
                };

                _contextDatabase.Movimientos.Add(movimiento);

                //Actualizamos saldoInicial de la cuenta
                cuenta.saldoInicial = saldoActual;
                _contextDatabase.Cuentas.Update(cuenta);

                //Guardamos
                await _contextDatabase.SaveChangesAsync();

                result = 1;
                return (result, "Retiro realizado corretamente");
            }
            catch (Exception ex)
            {
                result = 0;
                _logger.LogError($"Error al realizar el retiro, {ex}");
                return (result, "Error interno al realizar movimiento.");
            }
        }
    }
}
