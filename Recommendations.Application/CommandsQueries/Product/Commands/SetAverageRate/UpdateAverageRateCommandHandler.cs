using MediatR;
using Recommendations.Application.CommandsQueries.Product.Queries;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Product.Commands.SetAverageRate;

public class UpdateAverageRateCommandHandler
    : IRequestHandler<UpdateAverageRateCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public UpdateAverageRateCommandHandler(IRecommendationsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateAverageRateCommand request,
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
        var getProductQuery = new GetProductQuery
        {
            ProductId = productId
        };
        return await _mediator.Send(getProductQuery, cancellationToken);
    }

    private double CalculateAverageRate(Domain.Product product)
    {
        var ratings = product.UserRatings.Select(r => r.Value);
        return ratings.Average();
    }
}