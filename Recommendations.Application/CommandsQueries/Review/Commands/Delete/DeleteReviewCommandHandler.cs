using MediatR;
using Recommendations.Application.CommandsQueries.Like.Commands.SetUserLikesCount;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Delete;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;
    private readonly IFirebaseService _firebaseService;

    public DeleteReviewCommandHandler(IRecommendationsDbContext context,
        IMediator mediator, IFirebaseService firebaseService)
    {
        _context = context;
        _mediator = mediator;
        _firebaseService = firebaseService;
    }

    public async Task<Unit> Handle(DeleteReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);
        
        await SetUserLikesCount(review.User.Id, cancellationToken);
        if (review.Images is not null && review.Images.Any())
            await _firebaseService.DeleteFolder(review.Images[0].FolderName);

        return Unit.Value;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewQuery = new GetReviewQuery(reviewId);
        return await _mediator.Send(getReviewQuery, cancellationToken);
    }
    
    private async Task SetUserLikesCount(Guid userId,
        CancellationToken cancellationToken)
    {
        var setUserLikeQuery = new SetUserLikesCountQuery(userId);
        await _mediator.Send(setUserLikeQuery, cancellationToken);
    }
}