using CasoPractico.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasoPractico.Infrastructure.Validators
{
    public class EstadoCuentaDtoValidator : AbstractValidator<EstadoCuentaDto>
    {
        public EstadoCuentaDtoValidator()
        {
            RuleFor(x => x.fechaInicial).NotNull().NotEmpty();
            RuleFor(x => x.fechaFinal).NotNull().NotEmpty();
            RuleFor(x => x.clienteId).NotNull().NotEmpty();
        }
    }
}
