using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetRecentList;

public class GetRecentReviewsQueryHandler
    : IRequestHandler<GetRecentReviewsQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetRecentReviewsQueryHandler(IRecommendationsDbContext context,
        IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<GetAllReviewsVm> Handle(GetRecentReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Category)
            .Include(r => r.Product.UserRatings)
            .OrderByDescending(r => r.CreationDate)
            .Take(request.Count)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllReviewsVm { Reviews = reviews };
    }
}