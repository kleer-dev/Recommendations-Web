using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Queries.GetAllUserLikesCount;

public class GetAllUserLikesCountQueryHandler
    : IRequestHandler<GetAllUserLikesCountQuery, int>
{
    private readonly IRecommendationsDbContext _context;

    public GetAllUserLikesCountQueryHandler(IRecommendationsDbContext context) =>
        _context = context;

    public async Task<int> Handle(GetAllUserLikesCountQuery request,
        CancellationToken cancellationToken)
    {
        var likesCount = await _context.Likes
            .Where(l => l.Review.User.Id == request.UserId)
            .CountAsync(l => l.Status, cancellationToken);
        return likesCount;
    }
}