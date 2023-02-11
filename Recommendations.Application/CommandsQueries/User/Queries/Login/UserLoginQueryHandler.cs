using MediatR;
using Microsoft.AspNetCore.Identity;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Common.Exceptions;
using AuthenticationException = Recommendations.Application.Common.Exceptions.AuthenticationException;

namespace Recommendations.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, Unit>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public UserLoginQueryHandler(UserManager<Domain.User> userManager,
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Unit> Handle(UserLoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await CheckUser(request);
        await _signInManager.SignInAsync(user, request.Remember);

        return Unit.Value;
    }

    private async Task<Domain.User> CheckUser(UserLoginQuery request)
    {
        var user = await GetUserByEmail(request.Email);
        await CheckUserPassword(user, request.Password);
        if (user.AccessStatus == UserAccessStatuses.Blocked)
            throw new AccessDeniedException($"The user with id: {user.Id} has been blocked");

        return user;
    }

    private async Task<Domain.User> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new NotFoundException(nameof(User), email);

        return user;
    }

    private async Task CheckUserPassword(Domain.User user, string password)
    {
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordCorrect)
            throw new AuthenticationException("Invalid password");
    }
}