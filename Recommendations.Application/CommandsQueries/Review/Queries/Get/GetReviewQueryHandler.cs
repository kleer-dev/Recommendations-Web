using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.Get;

public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, Domain.Review>
{
    private readonly IRecommendationsDbContext _context;

    public GetReviewQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Review> Handle(GetReviewQuery request,
        CancellationToken cancellationToken)
    {
        var review = await _context.Reviews
            .Include(r => r.Product)
            .ThenInclude(r => r.UserRatings)
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Likes)
            .Include(r => r.Tags)
            .Include(r => r.Images)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        if (review is null)
            throw new NotFoundException("Review not found");

        return review;
    }
}