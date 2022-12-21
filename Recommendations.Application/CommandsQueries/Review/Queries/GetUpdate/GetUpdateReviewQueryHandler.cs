using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetUpdate;

public class GetUpdateReviewQueryHandler
    : IRequestHandler<GetUpdateReviewQuery, GetUpdateReviewDto>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetUpdateReviewQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUpdateReviewDto> Handle(GetUpdateReviewQuery request,
        CancellationToken cancellationToken)
    {
        var review = await _context.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Product)
            .Include(r => r.Category)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        if (review is null)
            throw new NullReferenceException($"The review with id {request.ReviewId} not found");

        return _mapper.Map<GetUpdateReviewDto>(review);
    }
}