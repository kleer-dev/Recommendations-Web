using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Commands.Set;

public class SetLikeCommand: IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid ReviewId { get; set; }
    public bool IsLike { get; set; }

    public SetLikeCommand(Guid userId, Guid reviewId, bool isLike)
    {
        UserId = userId;
        ReviewId = reviewId;
        IsLike = isLike;
    }
}