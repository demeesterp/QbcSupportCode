using FluentValidation;
using QbcBackend.Molecules.Entities;
using System;

namespace QbcBackend.Molecules.Validations
{

    public enum CalculationErrors
    {
        CalculationCodeMandatory,
        CalculationCodeSmallerThen100,
        CalculationDescriptionSmallerThan1000,
    }

    public class CalculationValidator : AbstractValidator<Calculation>
    {
        public CalculationValidator()
        {
            RuleFor(item => item.Code).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(CalculationErrors.CalculationCodeMandatory.ToString())
                    .Length(1, 100).WithErrorCode(CalculationErrors.CalculationCodeSmallerThen100.ToString());

            RuleFor(item => item.Description).Length(1, 2500).WithErrorCode(CalculationErrors.CalculationDescriptionSmallerThan1000.ToString())
                    .When(i => !String.IsNullOrWhiteSpace(i.Description));

        }


    }
}
