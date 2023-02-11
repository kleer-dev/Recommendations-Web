using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Commands.Create;

public class CreateLikeCommand : IRequest<Domain.Like>
{
    public Guid UserId { get; set; }
    public Guid ReviewId { get; set; }

    public CreateLikeCommand(Guid userId, Guid reviewId)
    {
        UserId = userId;
        ReviewId = reviewId;
    }
}