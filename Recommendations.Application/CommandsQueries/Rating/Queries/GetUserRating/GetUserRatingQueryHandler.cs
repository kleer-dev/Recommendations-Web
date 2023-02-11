using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Rating.Queries.GetUserRating;

public class GetUserRatingQueryHandler : IRequestHandler<GetUserRatingQuery, Domain.Rating?>
{
    private readonly IRecommendationsDbContext _context;

    public GetUserRatingQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Rating?> Handle(GetUserRatingQuery request,
        CancellationToken cancellationToken)
    {
        var rating = await _context.Ratings
            .Include(r => r.User)
            .Include(r => r.Product)
            .Where(r => r.User.Id == request.UserId
                        && r.Product.Reviews.Any(p => p.Id == request.ReviewId))
            .FirstOrDefaultAsync(cancellationToken);

        return rating;
    }
}