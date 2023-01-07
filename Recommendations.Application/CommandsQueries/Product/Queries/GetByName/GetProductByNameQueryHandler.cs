using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Product.Queries.GetByName;

public class GetProductByNameQueryHandler
    : IRequestHandler<GetProductByNameQuery, Domain.Product?>
{
    private readonly IRecommendationsDbContext _context;

    public GetProductByNameQueryHandler(IRecommendationsDbContext context) =>
        _context = context;

    public async Task<Domain.Product?> Handle(GetProductByNameQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Name == request.ProductName, cancellationToken);
        
        return product;
    }
}