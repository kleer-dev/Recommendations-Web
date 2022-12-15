using MediatR;

namespace Recommendations.Application.CommandsQueries.Like.Queries.Get;

public class GetLikeQuery : IRequest<Domain.Like>
{
    public Guid? UserId { get; set; }
    public Guid ReviewId { get; set; }
}