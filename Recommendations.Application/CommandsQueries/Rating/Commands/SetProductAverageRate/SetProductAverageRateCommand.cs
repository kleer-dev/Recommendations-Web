using MediatR;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.SetProductAverageRate;

public class SetProductAverageRateCommand : IRequest
{
    public Guid ProductId { get; set; }

    public SetProductAverageRateCommand(Guid productId)
    {
        ProductId = productId;
    }
}