using FluentValidation;
using QbcBackend.Molecules.Entities;
using System;

namespace QbcBackend.Molecules.Validations
{
    public enum CategoryErrors
    {
        CategoryCodeMandatory,
        CategoryCodeSmallerThen100,
        CategoryDescriptionSmallerThan1000
    }

    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(item => item.Code).Must(i => !String.IsNullOrWhiteSpace(i))
                .WithErrorCode(CategoryErrors.CategoryCodeMandatory.ToString())
                    .Length(1, 100).WithErrorCode(CategoryErrors.CategoryCodeSmallerThen100.ToString());

            RuleFor(item => item.Description).Length(1, 2500).WithErrorCode(CategoryErrors.CategoryDescriptionSmallerThan1000.ToString())
                    .When(i => !String.IsNullOrWhiteSpace(i.Description));
        }
    }
}
