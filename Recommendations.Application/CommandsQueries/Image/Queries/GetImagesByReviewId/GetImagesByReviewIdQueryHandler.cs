using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Image.Queries.GetImagesByReviewId;

public class GetImagesByReviewIdQueryHandler
    : IRequestHandler<GetImagesByReviewIdQuery, List<Domain.Image>?>
{
    private readonly IRecommendationsDbContext _context;

    public GetImagesByReviewIdQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Image>?> Handle(GetImagesByReviewIdQuery request,
        CancellationToken cancellationToken)
    {
        var images = await _context.Images
            .Where(i => i.Review.Id == request.ReviewId)
            .ToListAsync(cancellationToken);

        return images;
    }
}