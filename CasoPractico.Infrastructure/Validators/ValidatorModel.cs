using FluentValidation.Results;

namespace CasoPractico.Infrastructure.Validators
{
    public class ValidatorModel
    {
        public string campo { get; }

        public string errorMsg { get; }

        public ValidatorModel(ValidationFailure error)
        {
            campo = error.PropertyName;
            errorMsg = error.ErrorMessage;
        }

    }
}
