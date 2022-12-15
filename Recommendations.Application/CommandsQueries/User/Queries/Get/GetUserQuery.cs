using MediatR;

namespace Recommendations.Application.CommandsQueries.User.Queries.Get;

public class GetUserQuery : IRequest<Domain.User>
{
    public Guid? UserId { get; set; }
}