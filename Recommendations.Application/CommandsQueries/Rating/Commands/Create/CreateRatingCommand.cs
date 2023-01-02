using MediatR;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.Create;

public class CreateRatingCommand : IRequest<Domain.Rating>
{
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
    public int RatingValue { get; set; }

    public CreateRatingCommand(Guid? userId, Guid productId, int ratingValue)
    {
        UserId = userId;
        ProductId = productId;
        RatingValue = ratingValue;
    }
}