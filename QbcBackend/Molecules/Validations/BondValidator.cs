using FluentValidation;
using QbcBackend.Molecules.Entities;

namespace QbcBackend.Molecules.Validations
{

    public enum BondErrors
    {

    }

    public class BondValidator : AbstractValidator<Bond>
    {
        public BondValidator()
        {

        }
    }
}
