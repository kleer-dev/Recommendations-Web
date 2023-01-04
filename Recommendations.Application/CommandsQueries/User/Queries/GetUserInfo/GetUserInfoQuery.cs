using MediatR;

namespace Recommendations.Application.CommandsQueries.User.Queries.GetUserInfo;

public class GetUserInfoQuery : IRequest<GetUserDto>
{
    public Guid UserId { get; set; }

    public GetUserInfoQuery(Guid userId)
    {
        UserId = userId;
    }
}