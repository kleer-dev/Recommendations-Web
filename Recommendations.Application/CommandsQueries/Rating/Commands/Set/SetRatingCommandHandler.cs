using MediatR;
using Recommendations.Application.CommandsQueries.Rating.Commands.Create;
using Recommendations.Application.CommandsQueries.Rating.Commands.SetProductAverageRate;
using Recommendations.Application.CommandsQueries.Rating.Queries.GetUserRating;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.Set;

public class SetRatingCommandHandler : IRequestHandler<SetRatingCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public SetRatingCommandHandler(IRecommendationsDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(SetRatingCommand request,
        CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        var rating = await GetRating(request.UserId, review.Product.Id, cancellationToken) 
                     ?? await CreateRating(request.UserId, review.Product.Id,
                         request.Value, cancellationToken);
        rating.Value = request.Value;
        
        await UpdateAverageRate(review.Product.Id, cancellationToken);
        _context.Ratings.Update(rating);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewCommand = new GetReviewQuery(reviewId);
        return await _mediator.Send(getReviewCommand, cancellationToken);
    }

    private async Task<Domain.Rating?> GetRating(Guid userId, Guid productId,
        CancellationToken cancellationToken)
    {
        var getRatingQuery = new GetUserRatingQuery(userId, productId);
        return await _mediator.Send(getRatingQuery, cancellationToken);;
    }

    private async Task<Domain.Rating> CreateRating(Guid userId, Guid productId, int value,
        CancellationToken cancellationToken)
    {
        var createRatingCommand = new CreateRatingCommand(userId, productId, value);
        return  await _mediator.Send(createRatingCommand, cancellationToken);
    }

    private async Task UpdateAverageRate(Guid productId,
        CancellationToken cancellationToken)
    {
        var updateAverageRateCommand = new SetProductAverageRateCommand(productId);
        await _mediator.Send(updateAverageRateCommand, cancellationToken);
    }
}