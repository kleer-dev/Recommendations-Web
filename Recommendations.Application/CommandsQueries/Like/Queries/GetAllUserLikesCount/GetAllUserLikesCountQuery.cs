using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Queries.GetAllUserLikesCount;

public class GetAllUserLikesCountQuery : IRequest<int>
{
    public Guid? UserId { get; set; }

    public GetAllUserLikesCountQuery(Guid? userId)
    {
        UserId = userId;
    }
}