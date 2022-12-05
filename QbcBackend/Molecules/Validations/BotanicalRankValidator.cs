using FluentValidation;
using QbcBackend.Molecules.Model.Botany;
using System;

namespace QbcBackend.Botany.Validation
{
    public class BotanicalRankValidator : AbstractValidator<BotanicalRank>
    {
        public BotanicalRankValidator()
        {
            RuleFor(item => item.Name).Must(i => !String.IsNullOrWhiteSpace(i))
                    .WithErrorCode(BotanicalRankErrors.NameMandatory.ToString())
                        .Length(1, 250).WithErrorCode(BotanicalRankErrors.NameSmallerThen250.ToString());


            RuleFor(item => item.Description).Length(1, 2500).WithErrorCode(BotanicalRankErrors.DescriptionSmallerThen2500.ToString())
                                .When(i => !String.IsNullOrWhiteSpace(i.Description));



            RuleFor(item => item.ParentName).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(BotanicalRankErrors.ParentNameMandatory.ToString())
                    .Length(1, 250).WithErrorCode(BotanicalRankErrors.ParentNameSmallerThen250.ToString());
        }
    }


    public enum BotanicalRankErrors
    {
        NameMandatory = 1,
        NameSmallerThen250 = 2,
        DescriptionSmallerThen2500 = 3,
        ParentNameMandatory = 4,
        ParentNameSmallerThen250 = 5
    }
}
