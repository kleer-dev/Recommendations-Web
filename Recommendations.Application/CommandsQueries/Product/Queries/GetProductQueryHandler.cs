using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Product.Queries;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Domain.Product>
{
    private readonly IRecommendationsDbContext _context;

    public GetProductQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Product> Handle(GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        if (product is null)
            throw new NullReferenceException("The product not found");
        
        return product;
    }
}