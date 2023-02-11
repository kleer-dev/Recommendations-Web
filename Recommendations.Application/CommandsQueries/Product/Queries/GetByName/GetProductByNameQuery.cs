using MediatR;

namespace Recommendations.Application.CommandsQueries.Product.Queries.GetByName;

public class GetProductByNameQuery : IRequest<Domain.Product?>
{
    public string ProductName { get; set; }

    public GetProductByNameQuery(string productName)
    {
        ProductName = productName;
    }
}