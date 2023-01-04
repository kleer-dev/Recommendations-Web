using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsDtoByIdList;

public class GetReviewsDtoByIdListQueryHandler
    : IRequestHandler<GetReviewsDtoByIdListQuery, IEnumerable<GetAllReviewsDto>>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetReviewsDtoByIdListQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllReviewsDto>> Handle(GetReviewsDtoByIdListQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Where(r => request.IdList.Contains(r.Id))
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return reviews;
    }
}