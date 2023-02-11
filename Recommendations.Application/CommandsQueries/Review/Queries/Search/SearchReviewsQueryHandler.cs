using MediatR;
using Recommendations.Application.CommandsQueries.Image.Queries.GetFirstImageUrlByReviewId;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsDtoByIdList;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.Search;

public class SearchReviewsQueryHandler
    : IRequestHandler<SearchReviewsQuery, GetAllReviewsVm>
{
    private readonly IAlgoliaService _algoliaService;
    private readonly IMediator _mediator;

    public SearchReviewsQueryHandler(IAlgoliaService algoliaService,
        IMediator mediator)
    {
        _algoliaService = algoliaService;
        _mediator = mediator;
    }

    public async Task<GetAllReviewsVm> Handle(SearchReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var searchReviewIds = await _algoliaService.Search(request.SearchQuery);
        var reviews = await GetReviewsByIdList(searchReviewIds, cancellationToken);
        foreach (var review in reviews)
            review.ImageUrl = await GetFirstImageUrl(review.Id, cancellationToken);

        return new GetAllReviewsVm {Reviews = reviews};
    }
    
    private async Task<string?> GetFirstImageUrl(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getFirstImageByReviewId = new GetFirstImageUrlByReviewIdQuery(reviewId);
        return await _mediator.Send(getFirstImageByReviewId, cancellationToken);
    }

    private async Task<List<GetAllReviewsDto>> GetReviewsByIdList(IEnumerable<Guid> idList,
        CancellationToken cancellationToken)
    {
        var getReviewsByIdList = new GetReviewsDtoByIdListQuery(idList);
        var reviews = await _mediator.Send(getReviewsByIdList, cancellationToken);
        return reviews.ToList();
    }
}