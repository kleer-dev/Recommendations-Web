using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.CommandsQueries.Review.Queries.GetMostRatedList;
using Recommendations.Application.CommandsQueries.Review.Queries.GetRecentList;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByParam;

public class GetReviewsByParamQueryHandler
    : IRequestHandler<GetReviewsByParamQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public GetReviewsByParamQueryHandler(IRecommendationsDbContext context, 
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<GetAllReviewsVm> Handle(GetReviewsByParamQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = request.Filtrate switch
        {
            FiltrateParameters.MostRated =>
                await GetMostRatedReviews(request.Count, cancellationToken),
            FiltrateParameters.Recent =>
                await GetRecentReviews(request.Count, cancellationToken),
            _ => throw new ArgumentOutOfRangeException()
        };
        if (request.Tag is not null)
            reviews = reviews.Where(r => 
                r.Tags.Contains(request.Tag)).ToList();

        return new GetAllReviewsVm { Reviews = reviews };
    }

    private async Task<List<GetAllReviewsDto>> GetRecentReviews(int? count,
        CancellationToken cancellationToken)
    {
        var getRecentReviewsQuery = new GetRecentReviewsQuery(count);
        var reviewsVm = await _mediator.Send(getRecentReviewsQuery, cancellationToken);
        return reviewsVm.Reviews.ToList();
    }

    private async Task<List<GetAllReviewsDto>> GetMostRatedReviews(int? count,
        CancellationToken cancellationToken)
    {
        var getMostRatedReviewsQuery = new GetMostRatedListQuery(count);
        var reviewsVm = await _mediator.Send(getMostRatedReviewsQuery, cancellationToken);
        return reviewsVm.Reviews.ToList();
    }
}