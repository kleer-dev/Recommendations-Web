using FluentValidation;

namespace Recommendations.Application.CommandsQueries.Product.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Name).MinimumLength(5);
        RuleFor(p => p.Name).MaximumLength(100);
    }
}