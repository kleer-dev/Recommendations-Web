using MediatR;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetLinkedReviewsByReviewId;

public class GetLinkedReviewsByReviewIdQuery : IRequest<IEnumerable<GetLinkedReviewsDto>>
{
    public Guid ReviewId { get; set; }

    public GetLinkedReviewsByReviewIdQuery(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}