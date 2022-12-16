using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetRecentList;

public class GetRecentReviewsQuery : IRequest<GetAllReviewsVm>
{
    public int Count { get; set; }
}