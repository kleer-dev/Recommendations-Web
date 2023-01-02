using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Commands.GetOrCreate;

public class GetOrCreateLikeCommandHandler
    : IRequestHandler<GetOrCreateLikeCommand, Domain.Like>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public GetOrCreateLikeCommandHandler(IRecommendationsDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Domain.Like> Handle(GetOrCreateLikeCommand request,
        CancellationToken cancellationToken)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(l =>
            l.Review.Id == request.ReviewId &&
            l.User.Id == request.UserId, cancellationToken)
                   ?? await CreateLike(request, cancellationToken);
        
        return like;
    }

    private async Task<Domain.Like> CreateLike(GetOrCreateLikeCommand command,
        CancellationToken cancellationToken)
    {
        var like = new Domain.Like 
        {
            Review = await GetReview(command.ReviewId, cancellationToken),
            Status = command.IsLike ?? false,
            User = await GetUser(command.UserId, cancellationToken)
        };
        await _context.Likes.AddAsync(like, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return like;
    }
    
    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewCommand = new GetReviewQuery(reviewId);
        return await _mediator.Send(getReviewCommand, cancellationToken);
    }
    
    private async Task<Domain.User> GetUser(Guid? userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery, cancellationToken);
    }
}