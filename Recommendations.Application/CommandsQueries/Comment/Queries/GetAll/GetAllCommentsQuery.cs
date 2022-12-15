using MediatR;

namespace Recommendations.Application.CommandsQueries.Comment.Queries.GetAll;

public class GetAllCommentsQuery : IRequest<GetAllCommentsVm>
{
    public Guid ReviewId { get; set; }
}