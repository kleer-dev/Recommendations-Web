using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByUserId;

public class GetReviewsByUserIdQueryHandler
    : IRequestHandler<GetReviewsByUserIdQuery, GetReviewsByUserIdVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetReviewsByUserIdQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetReviewsByUserIdVm> Handle(GetReviewsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(r => r.Category)
            .Include(r => r.Comments)
            .Include(r => r.Likes)
            .Include(r => r.Product)
            .ProjectTo<GetReviewsByUserIdDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetReviewsByUserIdVm { Reviews = reviews };
    }
}