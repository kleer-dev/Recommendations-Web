using MediatR;

namespace Recommendations.Application.CommandsQueries.Rating.Queries.GetUserRating;

public class GetUserRatingQuery : IRequest<Domain.Rating?>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public GetUserRatingQuery(Guid userId, Guid productId)
    {
        UserId = userId;
        ProductId = productId;
    }
}