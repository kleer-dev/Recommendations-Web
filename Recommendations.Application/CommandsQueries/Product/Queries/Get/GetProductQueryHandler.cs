using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Product.Queries.Get;

public class GetProductQueryHandler
    : IRequestHandler<GetProductQuery, Domain.Product>
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
            .Include(p => p.UserRatings)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        if (product is null)
            throw new NotFoundException("The product not found");
        
        return product;
    }
}