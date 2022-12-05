using FluentValidation;
using QbcBackend.Molecules.Entities;
using System;

namespace QbcBackend.Molecules.Validations
{
    public enum AtomOrbitalErrors
    {
        SymbolMandatory,
        SymbolSmallerThan100
    }

    public class AtomOrbitalValidator : AbstractValidator<AtomOrbital>
    {
        public AtomOrbitalValidator()
        {
            RuleFor(item => item.Symbol).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(AtomOrbitalErrors.SymbolMandatory.ToString())
                    .Length(1, 100).WithErrorCode(AtomOrbitalErrors.SymbolSmallerThan100.ToString());
        }

    }
}
