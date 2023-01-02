using MediatR;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsDtoByIdList;

public class GetReviewsDtoByIdListQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
    public IEnumerable<Guid> IdList { get; set; }

    public GetReviewsDtoByIdListQuery(IEnumerable<Guid> idList)
    {
        IdList = idList;
    }
}