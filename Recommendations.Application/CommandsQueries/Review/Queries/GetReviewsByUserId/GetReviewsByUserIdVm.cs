namespace Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByUserId;

public class GetReviewsByUserIdVm
{
    public IEnumerable<GetReviewsByUserIdDto> Reviews { get; set; }
}