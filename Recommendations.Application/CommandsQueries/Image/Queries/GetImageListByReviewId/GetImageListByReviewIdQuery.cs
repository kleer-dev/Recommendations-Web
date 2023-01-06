using MediatR;

namespace Recommendations.Application.CommandsQueries.Image.Queries.GetImageListByReviewId;

public class GetImageListByReviewIdQuery : IRequest<IEnumerable<Domain.Image>>
{
    public Guid ReviewId { get; set; }

    public GetImageListByReviewIdQuery(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}