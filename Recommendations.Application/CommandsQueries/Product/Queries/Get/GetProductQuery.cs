using MediatR;

namespace Recommendations.Application.CommandsQueries.Product.Queries.Get;

public class GetProductQuery : IRequest<Domain.Product>
{
    public Guid ProductId { get; set; }

    public GetProductQuery(Guid productId)
    {
        ProductId = productId;
    }
}