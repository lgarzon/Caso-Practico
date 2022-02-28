using CasoPractico.Infrastructure.Repositories;
using System;
using System.Linq;

namespace CasoPractico.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(ContextDatabase context)
        {
            context.Database.EnsureCreated();

            //Clientes
            if (context.Clientes.Any())
            {
                return;   // DB has been seeded
            }

            var clientes = new Core.Entities.Cliente[]
            {
                new Core.Entities.Cliente{
                    nombre = "Jose Lema", 
                    direccion = "Otavalo sn y principal",
                    telefono = "098254785",
                    contrasenia = "1234",
                    estado = true,
                    edad = 25,
                    genero = "Masculino",
                    identificacion = "0123456505"
                },
            };

            foreach (Core.Entities.Cliente s in clientes)
            {
                context.Clientes.Add(s);
            }

            //Cuentas
            if (context.Cuentas.Any())
            {
                return;   // DB has been seeded
            }

            var cuentas = new Core.Entities.Cuenta[]
            {
                new Core.Entities.Cuenta{
                    numeroCuenta = 478758,
                    tipoCuenta = "Ahorro",
                    saldoInicial = 2000,
                    estado = true,
                    clienteId = 1
                },
            };

            foreach (Core.Entities.Cuenta s in cuentas)
            {
                context.Cuentas.Add(s);
            }

            //Cupo Diario
            if (context.CuposDiarios.Any())
            {
                return;   // DB has been seeded
            }

            var cupos = new Core.Entities.CupoDiario[]
            {
                new Core.Entities.CupoDiario{
                    fecha = DateTime.Now,
                    cupo = 1000
                },
                new Core.Entities.CupoDiario{
                    fecha = DateTime.Now.AddDays(1),
                    cupo = 1000
                },
                new Core.Entities.CupoDiario{
                    fecha = DateTime.Now.AddDays(2),
                    cupo = 1000
                },
                new Core.Entities.CupoDiario{
                    fecha = DateTime.Now.AddDays(3),
                    cupo = 1000
                },
            };

            foreach (Core.Entities.CupoDiario s in cupos)
            {
                context.CuposDiarios.Add(s);
            }

            context.SaveChanges();
        }
    }
}
