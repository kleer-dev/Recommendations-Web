using MediatR;

namespace Recommendations.Application.CommandsQueries.Image.Queries.GetImagesByReviewId;

public class GetImagesByReviewIdQuery : IRequest<List<Domain.Image>?>
{
    public Guid ReviewId { get; set; }
}