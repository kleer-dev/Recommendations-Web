using Algolia.Search.Clients;
using MediatR;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public DeleteUserCommandHandler(IMediator mediator,
        IRecommendationsDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(request.UserId);
        var user = await _mediator.Send(getUserQuery, cancellationToken);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}