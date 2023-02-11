using FluentValidation;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(command => command.UserId).NotNull();
        
        RuleFor(command => command.Title)
            .MinimumLength(5)
            .MaximumLength(100);
        
        RuleFor(command => command.ProductName)
            .MinimumLength(5)
            .MaximumLength(100);
        
        RuleFor(command => command.CategoryName).NotEmpty();
        
        RuleFor(command => command.Description)
            .MinimumLength(100)
            .MaximumLength(20000);

        RuleFor(command => command.Tags).NotEmpty();
    }
}