using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetAll;

public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, GetAllReviewsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetAllReviewsQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllReviewsVm> Handle(GetAllReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Category)
            .Include(r => r.Product.UserRatings)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllReviewsVm { Reviews = reviews };
    }
}