using FluentValidation;
using QbcBackend.Molecules.Model.Botany;
using System;

namespace QbcBackend.Molecules.Validations
{
    public class BotanicalNameInfoValidator : AbstractValidator<BotanicalNameInfo>
    {
        public BotanicalNameInfoValidator()
        {
            RuleFor(item => item.Name).Must(i => !String.IsNullOrWhiteSpace(i))
                    .WithErrorCode(BotanicalNameInfoErrors.NameMandatory.ToString())
                        .Length(1, 250).WithErrorCode(BotanicalNameInfoErrors.NameSmallerThen250.ToString());


            RuleFor(item => item.Description).Length(1, 2500)
                    .WithErrorCode(BotanicalNameInfoErrors.DescriptionSmallerThen2500.ToString())
                        .When(i => !String.IsNullOrWhiteSpace(i.Description));


            RuleFor(item => item.ParentName).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(BotanicalNameInfoErrors.ParentNameMandatory.ToString())
                    .Length(1, 250).WithErrorCode(BotanicalNameInfoErrors.ParentNameSmallerThen250.ToString());



            RuleFor(item => item.Rank).Must(i => !String.IsNullOrWhiteSpace(i))
                        .WithErrorCode(BotanicalNameInfoErrors.RankMandatory.ToString());

        }
    }

    public enum BotanicalNameInfoErrors
    {
        NameMandatory = 1,
        NameSmallerThen250 = 2,
        DescriptionSmallerThen2500 = 3,
        ParentNameMandatory = 4,
        ParentNameSmallerThen250 = 5,
        RankMandatory = 6
    }
}
