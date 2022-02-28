using CasoPractico.Core.DTOs;
using FluentValidation;
using System.Collections.Generic;

namespace CasoPractico.Infrastructure.Validators
{
    public class MovimientoDtoValidator : AbstractValidator<MovimientoDto>
    {
        List<string> tiposMovimientos = new List<string>() { "Deposito", "Retiro" };

        public MovimientoDtoValidator()
        {
            RuleFor(x => x.numeroCuenta).NotEmpty();
            RuleFor(x => x.tipoMovimiento).Must(x => tiposMovimientos.Contains(x))
                .WithMessage("El tipo de movimiento debería ser Deposito o Retiro");
            RuleFor(x => x.valor).NotEmpty();
        }
    }
}
