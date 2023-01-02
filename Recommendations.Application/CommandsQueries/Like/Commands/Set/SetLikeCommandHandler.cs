using MediatR;
using Recommendations.Application.CommandsQueries.Like.Commands.GetOrCreate;
using Recommendations.Application.CommandsQueries.Like.Commands.SetUserLikesCount;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Commands.Set;

public class SetLikeCommandHandler : IRequestHandler<SetLikeCommand, Guid>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public SetLikeCommandHandler(IRecommendationsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(SetLikeCommand request,
        CancellationToken cancellationToken)
    {
        var like = await GetOrCreateLike(request, cancellationToken);
        like.Status = request.IsLike;

        _context.Likes.Update(like);
        await _context.SaveChangesAsync(cancellationToken);
        
        var review = await GetReview(request.ReviewId, cancellationToken);
        await SetUserLikesCount(review.User.Id, cancellationToken);
        
        return like.Id;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewQuery = new GetReviewQuery(reviewId);
        return await _mediator.Send(getReviewQuery, cancellationToken);
    }

    private async Task<Domain.Like> GetOrCreateLike(SetLikeCommand request,
        CancellationToken cancellationToken)
    {
        var getOrCreateLikeCommand = new GetOrCreateLikeCommand(request.UserId,
            request.ReviewId, request.IsLike);
        return await _mediator.Send(getOrCreateLikeCommand, cancellationToken);
    }

    private async Task SetUserLikesCount(Guid userId,
        CancellationToken cancellationToken)
    {
        var setUserLikeQuery = new SetUserLikesCountQuery(userId);
        await _mediator.Send(setUserLikeQuery, cancellationToken);
    }
}