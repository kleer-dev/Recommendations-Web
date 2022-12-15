using MediatR;
using Recommendations.Application.CommandsQueries.Rating.Commands.Create;
using Recommendations.Application.CommandsQueries.Rating.Queries.GetUserRating;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.Set;

public class SetRatingCommandHandler : IRequestHandler<SetRatingCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public SetRatingCommandHandler(IRecommendationsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(SetRatingCommand request,
        CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        var productId = review.Product.Id;
        var rating = await GetRating(request, productId, cancellationToken);
        rating.Value = request.Value;
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var getReviewCommand = new GetReviewQuery
        {
            ReviewId = reviewId
        };
        var review = await _mediator.Send(getReviewCommand, cancellationToken);

        return review;
    }

    private async Task<Domain.Rating> GetRating(SetRatingCommand request, Guid productId,
        CancellationToken cancellationToken)
    {
        var getRatingQuery = new GetUserRatingQuery
        {
            UserId = request.UserId,
            ProductId = productId
        };
        var rating = await _mediator.Send(getRatingQuery, cancellationToken)
                     ?? await CreateRating(request, productId, cancellationToken);

        return rating;
    }

    private async Task<Domain.Rating> CreateRating(SetRatingCommand request, Guid productId,
        CancellationToken cancellationToken)
    {
        var createRatingCommand = new CreateRatingCommand
        {
            UserId = request.UserId,
            RatingValue = request.Value,
            ProductId = productId
        };
        var rating = await _mediator.Send(createRatingCommand, cancellationToken);

        return rating;
    }
}