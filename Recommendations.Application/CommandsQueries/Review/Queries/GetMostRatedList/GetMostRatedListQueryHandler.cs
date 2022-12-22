using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Review.Queries.GetAll;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetMostRatedList;

public class GetMostRatedListQueryHandler
    : IRequestHandler<GetMostRatedListQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetMostRatedListQueryHandler(IRecommendationsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllReviewsVm> Handle(GetMostRatedListQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Category)
            .Include(r => r.Product)
            .Include(r => r.Product.UserRatings)
            .OrderByDescending(r => r.Product.AverageRate)
            .Take(request.Count)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllReviewsVm { Reviews = reviews };
    }
}