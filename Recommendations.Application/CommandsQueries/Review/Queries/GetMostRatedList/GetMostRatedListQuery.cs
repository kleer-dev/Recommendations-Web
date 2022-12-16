using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetMostRatedList;

public class GetMostRatedListQuery : IRequest<GetAllReviewsVm>
{
    public int Count { get; set; }
}