using MediatR;
using Recommendations.Application.CommandsQueries.Product.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.SetProductAverageRate;

public class SetProductAverageRateCommandHandler
    : IRequestHandler<SetProductAverageRateCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public SetProductAverageRateCommandHandler(IRecommendationsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(SetProductAverageRateCommand request,
        CancellationToken cancellationToken)
    {
        var product = await GetProduct(request.ProductId, cancellationToken);
        product.AverageRate = CalculateAverageRate(product);
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Domain.Product> GetProduct(Guid productId,
        CancellationToken cancellationToken)
    {
        var getProductQuery = new GetProductQuery(productId);
        return await _mediator.Send(getProductQuery, cancellationToken);
    }

    private double CalculateAverageRate(Domain.Product product)
    {
        var ratings = product.UserRatings.Select(r => r.Value);
        return ratings.Average();
    }
}