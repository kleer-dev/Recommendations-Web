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

    public GetRecentReviewsQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllReviewsVm> Handle(GetRecentReviewsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Count is null)
            throw new NullReferenceException("Missing reviews count");
        
        var reviews = await _context.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Category)
            .Include(r => r.Product.UserRatings)
            .OrderByDescending(r => r.CreationDate)
            .Take(request.Count.Value)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllReviewsVm { Reviews = reviews };
    }
}