using CasoPractico.Api.Controllers;
using CasoPractico.Core.DTOs;
using CasoPractico.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CasoPractico.UnitTest
{
    [TestClass]
    public class MovimientosTest
    {
        [TestMethod]
        public async Task ValidarValorDepositoBad()
        {

            MovimientoDto movimiento = new MovimientoDto()
            {
                numeroCuenta = 478758,
                tipoMovimiento = "Deposito",
                valor = -400
            };

            (int codigo, string mensaje) = (0, "El valor para un depósito debe ser mayor a cero. Valor ingresado -1");

            var service = new Mock<IMovimientoService>();
            service.Setup(s => s.AddMovimiento(movimiento)).ReturnsAsync((codigo, mensaje));

            //(codigo, mensaje) = await service.Object.AddMovimiento(movimiento);

            var controller = new MovimientosController(service.Object);

            IActionResult actionResult = await controller.Add(movimiento);

            BadRequestObjectResult result = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task ValidarValorDepositoOk()
        {

            MovimientoDto movimiento = new MovimientoDto()
            {
                numeroCuenta = 478758,
                tipoMovimiento = "Deposito",
                valor = 400
            };

            (int codigo, string mensaje) = (1, "Deposito realizado correctamente");

            var service = new Mock<IMovimientoService>();
            service.Setup(s => s.AddMovimiento(movimiento)).ReturnsAsync((codigo, mensaje));

            //(codigo, mensaje) = await service.Object.AddMovimiento(movimiento);

            var controller = new MovimientosController(service.Object);

            IActionResult actionResult = await controller.Add(movimiento);

            OkObjectResult result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

    }
}
