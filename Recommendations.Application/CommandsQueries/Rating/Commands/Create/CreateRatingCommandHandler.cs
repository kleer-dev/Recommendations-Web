using MediatR;
using Recommendations.Application.CommandsQueries.Product.Queries;
using Recommendations.Application.CommandsQueries.Product.Queries.Get;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Rating.Commands.Create;

public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Domain.Rating>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public CreateRatingCommandHandler(IRecommendationsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Domain.Rating> Handle(CreateRatingCommand request,
        CancellationToken cancellationToken)
    {
        var rating = new Domain.Rating
        {
            User = await GetUser(request.UserId, cancellationToken),
            Product = await GetProduct(request.ProductId, cancellationToken)
        };
        await _context.Ratings.AddAsync(rating, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return rating;
    }
    
    private async Task<Domain.User> GetUser(Guid userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery, cancellationToken);;
    }
    
    private async Task<Domain.Product> GetProduct(Guid productId,
        CancellationToken cancellationToken)
    {
        var getProductQuery = new GetProductQuery(productId);
        return await _mediator.Send(getProductQuery, cancellationToken);;
    }
}