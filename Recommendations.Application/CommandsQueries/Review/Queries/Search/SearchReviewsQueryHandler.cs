using AutoMapper;
using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.Search;

public class SearchReviewsQueryHandler
    : IRequestHandler<SearchReviewsQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IAlgoliaService _algoliaService;
    private readonly IMapper _mapper;

    public SearchReviewsQueryHandler(IRecommendationsDbContext context,
        IAlgoliaService algoliaService, IMapper mapper)
    {
        _context = context;
        _algoliaService = algoliaService;
        _mapper = mapper;
    }

    public async Task<GetAllReviewsVm> Handle(SearchReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var searchReviews = await _algoliaService.Search(request.SearchQuery);
        var reviews = _mapper.Map<List<Domain.Review>, List<GetAllReviewsDto>>(searchReviews);

        return new GetAllReviewsVm {Reviews = reviews};
    }
}