using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Commands.Create;

public class CreateLikeCommandHandler
    : IRequestHandler<CreateLikeCommand, Domain.Like>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public CreateLikeCommandHandler(IRecommendationsDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Domain.Like> Handle(CreateLikeCommand request,
        CancellationToken cancellationToken)
    {
        var like = new Domain.Like 
        {
            Review = await GetReview(request.ReviewId, cancellationToken),
            Status = true,
            User = await GetUser(request.UserId, cancellationToken)
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
    
    private async Task<Domain.User> GetUser(Guid userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery, cancellationToken);
    }
}