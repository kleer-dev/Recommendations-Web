using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetLinkedReviewsByReviewId;

public class GetLinkedReviewsByReviewIdQueryHandler
    : IRequestHandler<GetLinkedReviewsByReviewIdQuery, IEnumerable<GetLinkedReviewsDto>>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetLinkedReviewsByReviewIdQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetLinkedReviewsDto>> Handle(GetLinkedReviewsByReviewIdQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Where(r => r.Id == request.ReviewId)
            .Select(r => r.Product)
            .SelectMany(p => p.Reviews)
            .Where(r => r.Id != request.ReviewId)
            .ProjectTo<GetLinkedReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return reviews;
    }
}