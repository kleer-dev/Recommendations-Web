using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Commands.GetOrCreate;

public class GetOrCreateLikeCommand : IRequest<Domain.Like>
{
    public Guid? UserId { get; set; }
    public Guid ReviewId { get; set; }
    public bool? IsLike { get; set; }

    public GetOrCreateLikeCommand(Guid? userId, Guid reviewId)
    {
        UserId = userId;
        ReviewId = reviewId;
    }

    public GetOrCreateLikeCommand(Guid? userId, Guid reviewId, bool isLike)
    {
        UserId = userId;
        ReviewId = reviewId;
        IsLike = isLike;
    }
}