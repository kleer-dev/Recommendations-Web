using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

namespace Recommendations.Application.CommandsQueries.Review.Queries.Search;

public class SearchReviewsQuery : IRequest<GetAllReviewsVm>
{
    public string SearchQuery { get; set; }
}