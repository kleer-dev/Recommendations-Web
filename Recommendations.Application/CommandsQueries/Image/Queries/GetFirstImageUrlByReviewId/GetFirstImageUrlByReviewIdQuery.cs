using MediatR;

namespace Recommendations.Application.CommandsQueries.Image.Queries.GetFirstImageUrlByReviewId;

public class GetFirstImageUrlByReviewIdQuery : IRequest<string?>
{
    public Guid ReviewId { get; set; }

    public GetFirstImageUrlByReviewIdQuery(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}