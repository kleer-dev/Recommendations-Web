using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Commands.SetRole;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Unit>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly IRecommendationsDbContext _context;

    public SetUserRoleCommandHandler(IMediator mediator,
        UserManager<Domain.User> userManager, IRecommendationsDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Unit> Handle(SetUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Role != Roles.User && request.Role != Roles.Admin)
            throw new NotFoundException("Role", request.Role);
        var user = await GetUser(_userManager.Users, request.UserId, cancellationToken);
        var userRoles = await _userManager.GetRolesAsync(user);
        
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRoleAsync(user, request.Role);

        return Unit.Value;
    }

    private async Task<Domain.User> GetUser(IQueryable<Domain.User> users, Guid userId,
        CancellationToken cancellationToken)
    {
        var user = await users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                   ?? throw new NotFoundException(nameof(Domain.User), userId);

        return user;
    }
}