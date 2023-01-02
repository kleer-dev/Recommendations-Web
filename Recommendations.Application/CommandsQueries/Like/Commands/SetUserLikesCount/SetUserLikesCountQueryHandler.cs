using MediatR;
using Recommendations.Application.CommandsQueries.Like.Queries.GetAllUserLikesCount;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Commands.SetUserLikesCount;

public class SetUserLikesCountQueryHandler
    : IRequestHandler<SetUserLikesCountQuery, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public SetUserLikesCountQueryHandler(IRecommendationsDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(SetUserLikesCountQuery request,
        CancellationToken cancellationToken)
    {
        var user = await GetUser(request.UserId, cancellationToken);
        user.LikesCount = await GetLikesCount(user.Id, cancellationToken);

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Domain.User> GetUser(Guid? userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery, cancellationToken);
    }
    
    private async Task<int> GetLikesCount(Guid userId,
        CancellationToken cancellationToken)
    {
        var getAllUserLikesCountQuery = new GetAllUserLikesCountQuery(userId);
        return await _mediator.Send(getAllUserLikesCountQuery, cancellationToken);
    }
}