using MediatR;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Commands.Unblock;

public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public UnblockUserCommandHandler(IRecommendationsDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UnblockUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await GetUser(request.UserId);
        user.AccessStatus = UserAccessStatuses.Unblocked;

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
    
    private async Task<Domain.User> GetUser(Guid userId)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery);
    }
}