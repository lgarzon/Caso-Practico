using CasoPractico.Core.Entities;
using FluentValidation;

namespace CasoPractico.Infrastructure.Validators
{
    public class CuentaValidator : AbstractValidator<Cuenta>
    {
        public CuentaValidator()
        {
            RuleFor(x => x.numeroCuenta).NotEmpty();
            RuleFor(x => x.tipoCuenta).NotEmpty();
            RuleFor(x => x.clienteId).NotEmpty();
            RuleFor(x => x.saldoInicial).ScalePrecision(2, 14);
        }
    }
}
