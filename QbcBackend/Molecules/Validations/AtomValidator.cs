using FluentValidation;
using QbcBackend.Molecules.Entities;
using System;

namespace QbcBackend.Molecules.Validations
{

    public enum AtomErrors
    {
        SymbolMandatory,
        SymbolSmallerThan100
    }


    public class AtomValidator : AbstractValidator<Atom>
    {
        public AtomValidator()
        {
            RuleFor(item => item.Symbol).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(AtomErrors.SymbolMandatory.ToString())
                    .Length(1, 100).WithErrorCode(AtomErrors.SymbolSmallerThan100.ToString());
        }
    }
}
