using Contracts.DTO;
using FluentValidation;

namespace WebApi.Filters
{
    public class CategoryValidator : AbstractValidator<CategoryApiDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.")
                .NotEqual("admin").WithMessage("Category name 'admin' is not allowed.");
        }
    }
}
