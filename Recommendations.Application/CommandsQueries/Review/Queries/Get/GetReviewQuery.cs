using MediatR;

namespace Recommendations.Application.CommandsQueries.Review.Queries.Get;

public class GetReviewQuery : IRequest<Domain.Review>
{
    public Guid ReviewId { get; set; }
}