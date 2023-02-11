using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Queries.GetLikeStatus;

public class GetLikeStatusQueryHandler
    : IRequestHandler<GetLikeStatusQuery, bool>
{
    private readonly IRecommendationsDbContext _context;

    public GetLikeStatusQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(GetLikeStatusQuery request,
        CancellationToken cancellationToken)
    {
        var like = await _context.Likes
            .FirstOrDefaultAsync(l => l.Review.Id == request.ReviewId
                                      && l.User.Id == request.UserId, cancellationToken);
        return like?.Status ?? false;
    }
}