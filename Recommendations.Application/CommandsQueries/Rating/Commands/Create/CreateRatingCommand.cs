using MediatR;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.Create;

public class CreateRatingCommand : IRequest<Domain.Rating>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public CreateRatingCommand(Guid userId, Guid productId)
    {
        UserId = userId;
        ProductId = productId;
    }
}