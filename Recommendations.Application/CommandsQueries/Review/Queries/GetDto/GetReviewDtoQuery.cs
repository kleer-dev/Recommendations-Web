using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDtoQuery : IRequest<GetReviewDto>
{
    public Guid ReviewId { get; set; }
    public Guid? UserId { get; set; }
}