using FluentValidation;
using QbcBackend.Molecules.Entities;
using System;

namespace Services.Molecules.Validations
{

    public enum ModelErrors
    {
        ModelCodeMandatory,
        ModelCodeSmallerThen100,
        ModelDescriptionSmallerThan1000,
        ChargeMustBeEqualOrGreaterThanZero
    }

    public class ModelValidator : AbstractValidator<MoleculeModel>
    {
        public ModelValidator()
        {
            RuleFor(item => item.Code).Must(i => !String.IsNullOrWhiteSpace(i))
                    .WithErrorCode(ModelErrors.ModelCodeMandatory.ToString())
                            .Length(1, 100).WithErrorCode(ModelErrors.ModelCodeSmallerThen100.ToString());

            RuleFor(item => item.Description).Length(1, 2500)
                    .WithErrorCode(ModelErrors.ModelDescriptionSmallerThan1000.ToString())
                            .When(i => !String.IsNullOrWhiteSpace(i.Description));

            RuleFor(item => item.Charge).GreaterThanOrEqualTo(0)
                    .WithErrorCode(ModelErrors.ChargeMustBeEqualOrGreaterThanZero.ToString());

        }
    }
}
