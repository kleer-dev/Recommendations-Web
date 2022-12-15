using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Queries.Get;

public class GetLikeQueryHandler : IRequestHandler<GetLikeQuery, Domain.Like>
{
    private readonly IRecommendationsDbContext _context;

    public GetLikeQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Like> Handle(GetLikeQuery request,
        CancellationToken cancellationToken)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(l =>
            l.Review.Id == request.ReviewId &&
            l.User.Id == request.UserId, cancellationToken);

        return like;
    }
}