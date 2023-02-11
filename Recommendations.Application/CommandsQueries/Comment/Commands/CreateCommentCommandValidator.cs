using FluentValidation;

namespace Recommendations.Application.CommandsQueries.Comment.Commands;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(command => command.Text).NotEmpty();
        RuleFor(command => command.Text).MaximumLength(400);
    }
}