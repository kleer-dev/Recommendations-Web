using MediatR;

namespace Recommendations.Application.CommandsQueries.User.Commands.Block;

public class BlockUserCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }

    public BlockUserCommand(Guid userId)
    {
        UserId = userId;
    }
}