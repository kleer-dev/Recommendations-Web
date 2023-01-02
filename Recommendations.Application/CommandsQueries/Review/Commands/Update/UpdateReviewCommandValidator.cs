using FluentValidation;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Update;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(command => command.Title)
            .MinimumLength(5)
            .MaximumLength(100);
        
        RuleFor(command => command.ProductName)
            .MinimumLength(5)
            .MaximumLength(100);
        
        RuleFor(command => command.Description)
            .MinimumLength(100)
            .MaximumLength(20000);
        
        RuleFor(command => command.CategoryName).NotEmpty();
        
        RuleFor(command => command.Tags).NotEmpty();
    }
}