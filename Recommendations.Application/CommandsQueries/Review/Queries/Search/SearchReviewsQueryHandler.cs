using AutoMapper;
using MediatR;
using Recommendations.Application.CommandsQueries.Image.Queries.GetImagesByReviewId;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.Search;

public class SearchReviewsQueryHandler
    : IRequestHandler<SearchReviewsQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IAlgoliaService _algoliaService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SearchReviewsQueryHandler(IRecommendationsDbContext context,
        IAlgoliaService algoliaService, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _algoliaService = algoliaService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<GetAllReviewsVm> Handle(SearchReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var searchReviews = await _algoliaService.Search(request.SearchQuery);
        var reviews = _mapper.Map<List<Domain.Review>, List<GetAllReviewsDto>>(searchReviews);
        
        await Parallel.ForEachAsync(reviews, cancellationToken, async (review, token) =>
        {
            review.ImageUrl = await GetFirstImageUrl(review.Id, cancellationToken);
        });

        return new GetAllReviewsVm {Reviews = reviews};
    }
    
    private async Task<string?> GetFirstImageUrl(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getImagesByReviewIdQuery = new GetImagesByReviewIdQuery
        {
            ReviewId = reviewId
        };
        var images = await _mediator.Send(getImagesByReviewIdQuery, cancellationToken);
        return images.Select(i => i.Url).FirstOrDefault();
    }
}