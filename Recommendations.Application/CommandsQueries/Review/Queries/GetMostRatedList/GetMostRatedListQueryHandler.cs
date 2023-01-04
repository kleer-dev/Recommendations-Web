using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Image.Queries.GetFirstImageUrlByReviewId;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetMostRatedList;

public class GetMostRatedListQueryHandler
    : IRequestHandler<GetMostRatedListQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetMostRatedListQueryHandler(IRecommendationsDbContext context,
        IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<GetAllReviewsVm> Handle(GetMostRatedListQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Count is null)
            throw new NullReferenceException("Missing reviews count");
        
        var reviews = await _context.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Category)
            .Include(r => r.Product)
            .Include(r => r.Product.UserRatings)
            .OrderByDescending(r => r.Product.AverageRate)
            .Take(request.Count.Value)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        foreach (var review in reviews)
            review.ImageUrl = await GetFirstImageUrl(review.Id, cancellationToken);
        
        return new GetAllReviewsVm { Reviews = reviews };
    }

    private async Task<string?> GetFirstImageUrl(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getFirstImageByReviewId = new GetFirstImageUrlByReviewIdQuery(reviewId);
        return await _mediator.Send(getFirstImageByReviewId, cancellationToken);
    }
}