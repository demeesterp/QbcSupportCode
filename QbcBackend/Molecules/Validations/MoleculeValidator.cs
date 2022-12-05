using FluentValidation;
using QbcBackend.Molecules.Entities;

namespace QbcBackend.Molecules.Validations
{

    public enum MoleculeErrors {    }


    public class MoleculeValidator : AbstractValidator<Molecule>
    {
        public MoleculeValidator()
        {
        }
    }
}
