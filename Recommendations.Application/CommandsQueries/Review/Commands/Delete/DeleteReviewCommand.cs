using MediatR;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Delete;

public class DeleteReviewCommand : IRequest
{
    public Guid ReviewId { get; set; }
}