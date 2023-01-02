using MediatR;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByUserId;

public class GetReviewsByUserIdQuery : IRequest<GetReviewsByUserIdVm>
{
    public Guid? UserId { get; set; }

    public GetReviewsByUserIdQuery(Guid? userId)
    {
        UserId = userId;
    }
}