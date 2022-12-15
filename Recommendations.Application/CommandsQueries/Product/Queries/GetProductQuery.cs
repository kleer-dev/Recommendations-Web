using MediatR;

namespace Recommendations.Application.CommandsQueries.Product.Queries;

public class GetProductQuery : IRequest<Domain.Product>
{
    public Guid ProductId { get; set; }
}