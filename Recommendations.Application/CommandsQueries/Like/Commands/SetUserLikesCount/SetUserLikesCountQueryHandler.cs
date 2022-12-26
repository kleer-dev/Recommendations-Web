using MediatR;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Interfaces;

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
        user.LikesCount = GetLikesCount(user);

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Domain.User> GetUser(Guid? userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery
        {
            UserId = userId
        };
        return await _mediator.Send(getUserQuery, cancellationToken);
    }
    
    private int GetLikesCount(Domain.User user)
    {
        var likesCount = _context.Likes
            .Where(l => l.Review.User.Id == user.Id)
            .Count(l => l.Status == true);
        
        return likesCount;
    }
}