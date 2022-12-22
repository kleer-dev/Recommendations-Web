using MediatR;

namespace Recommendations.Application.CommandsQueries.Product.Commands.SetAverageRate;

public class UpdateAverageRateCommand : IRequest
{
    public Guid ProductId { get; set; }
}