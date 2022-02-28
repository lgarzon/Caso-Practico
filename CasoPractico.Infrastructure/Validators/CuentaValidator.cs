using CasoPractico.Core.Entities;
using FluentValidation;
using System.Collections.Generic;

namespace CasoPractico.Infrastructure.Validators
{
    public class CuentaValidator : AbstractValidator<Cuenta>
    {
        List<string> tiposCuentas = new List<string>() { "Ahorro", "Corriente" };

        public CuentaValidator()
        {
            RuleFor(x => x.numeroCuenta).NotEmpty();
            RuleFor(x => x.tipoCuenta).Must(x => tiposCuentas.Contains(x))
                .WithMessage("El tipo de cuenta debería ser Ahorro o Corriente");
            RuleFor(x => x.clienteId).NotEmpty();
            RuleFor(x => x.saldoInicial).ScalePrecision(2, 14);
        }
    }
}
