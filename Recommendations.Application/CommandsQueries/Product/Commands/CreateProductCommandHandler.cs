using MediatR;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Product.Commands;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Domain.Product>
{
    private readonly IRecommendationsDbContext _context;

    public CreateProductCommandHandler(IRecommendationsDbContext context) =>
        _context = context;

    public async Task<Domain.Product> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Domain.Product
        {
            Name = request.Name
        };
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }
}