using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Review.Queries;
using Recommendations.Application.CommandsQueries.Review.Queries.Search;
using Recommendations.Domain;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : BaseController
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("reviews")]
    public async Task<ActionResult<IEnumerable<GetAllReviewsDto>>> FindReviews(string searchQuery)
    {
        var searchReviewsQuery = new SearchReviewsQuery { SearchQuery = searchQuery };
        var searchResults = await _mediator.Send(searchReviewsQuery);

        return searchResults.Reviews.ToList();
    }
}