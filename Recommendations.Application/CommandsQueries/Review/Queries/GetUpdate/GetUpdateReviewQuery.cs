using MediatR;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetUpdate;

public class GetUpdateReviewQuery : IRequest<GetUpdateReviewDto>
{
    public Guid ReviewId { get; set; }
}