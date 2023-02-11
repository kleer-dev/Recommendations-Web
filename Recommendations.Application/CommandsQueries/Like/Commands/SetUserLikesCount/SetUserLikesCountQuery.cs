using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Commands.SetUserLikesCount;

public class SetUserLikesCountQuery : IRequest
{
    public Guid UserId { get; set; }

    public SetUserLikesCountQuery(Guid userId)
    {
        UserId = userId;
    }
}