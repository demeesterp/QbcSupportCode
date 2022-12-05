using FluentValidation;
using QbcBackend.Molecules.Entities;
using System;

namespace QbcBackend.Molecules.Validations
{

    public enum CalculationGroupErrors
    {
        CalculationGroupNameManatory,
        CalculationGroupNameSmallerThen100
    }


    public class CalculationGroupValidator : AbstractValidator<CalculationGroup>
    {

        public CalculationGroupValidator()
        {
            RuleFor(item => item.Name).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(CalculationGroupErrors.CalculationGroupNameManatory.ToString())
                    .Length(1, 100).WithErrorCode(CalculationGroupErrors.CalculationGroupNameSmallerThen100.ToString());
        }


    }
}
