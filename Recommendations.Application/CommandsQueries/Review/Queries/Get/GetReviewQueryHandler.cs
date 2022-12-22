using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

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
            .Include(r => r.User)
            .Include(r => r.Category)
            .Include(r => r.Likes)
            .Include(r => r.Tags)
            .Include(r => r.Product.UserRatings)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        if (review is null)
            throw new NullReferenceException($"Review with id: {request.ReviewId} not found");

        return review;
    }
}