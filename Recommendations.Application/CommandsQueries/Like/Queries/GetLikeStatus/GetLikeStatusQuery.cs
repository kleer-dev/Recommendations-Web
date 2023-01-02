using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Queries.GetLikeStatus;

public class GetLikeStatusQuery : IRequest<bool>
{
    public Guid? UserId { get; set; }
    public Guid ReviewId { get; set; }

    public GetLikeStatusQuery(Guid? userId, Guid reviewId)
    {
        UserId = userId;
        ReviewId = reviewId;
    }
}