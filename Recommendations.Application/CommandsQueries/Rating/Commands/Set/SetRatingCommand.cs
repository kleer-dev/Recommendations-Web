using MediatR;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.Set;

public class SetRatingCommand : IRequest
{
    public int Value { get; set; }
    public Guid UserId { get; set; }
    public Guid ReviewId { get; set; }

    public SetRatingCommand(int value, Guid userId, Guid reviewId)
    {
        Value = value;
        UserId = userId;
        ReviewId = reviewId;
    }
}