using FluentValidation;
using QbcBackend.Molecules.Entities;

namespace QbcBackend.Molecules.Validations
{

    public enum BondAtomErrors
    {

    }

    public class BondAtomValidator : AbstractValidator<BondAtom>
    {
        public BondAtomValidator()
        {

        }
    }
}
