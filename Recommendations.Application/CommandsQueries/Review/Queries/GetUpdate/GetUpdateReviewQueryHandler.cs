using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

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
            .Include(r => r.Images)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        if (review is null)
            throw new NotFoundException("The review not found");

        return _mapper.Map<GetUpdateReviewDto>(review);
    }
}