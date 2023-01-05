using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Image.Queries.GetImageListByReviewId;

public class GetImageListByReviewIdQueryHandler
    : IRequestHandler<GetImageListByReviewIdQuery, IEnumerable<Domain.Image>>
{
    private readonly IRecommendationsDbContext _context;

    public GetImageListByReviewIdQueryHandler(IRecommendationsDbContext context) =>
        _context = context;

    public async Task<IEnumerable<Domain.Image>> Handle(GetImageListByReviewIdQuery request,
        CancellationToken cancellationToken)
    {
        var images = await _context.Images
            .Where(i => i.Review.Id == request.ReviewId)
            .ToListAsync(cancellationToken);

        return images;
    }
}