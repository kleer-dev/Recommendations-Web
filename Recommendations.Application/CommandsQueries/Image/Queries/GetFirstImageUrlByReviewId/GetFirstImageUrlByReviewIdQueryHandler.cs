using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Image.Queries.GetFirstImageUrlByReviewId;

public class GetFirstImageUrlByReviewIdQueryHandler
    : IRequestHandler<GetFirstImageUrlByReviewIdQuery, string?>
{
    private readonly IRecommendationsDbContext _context;

    public GetFirstImageUrlByReviewIdQueryHandler(IRecommendationsDbContext context) =>
        _context = context;

    public async Task<string?> Handle(GetFirstImageUrlByReviewIdQuery request,
        CancellationToken cancellationToken)
    {
        var images = await _context.Images
            .FirstOrDefaultAsync(image =>
                image.Review.Id == request.ReviewId, cancellationToken);

        return images?.Url;
    }
}