using MediatR;
using Microsoft.AspNetCore.Identity;
using Recommendations.Application.Common.Exceptions;
using AuthenticationException = Recommendations.Application.Common.Exceptions.AuthenticationException;

namespace Recommendations.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, Unit>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public UserLoginQueryHandler(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Unit> Handle(UserLoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await ConfirmUser(request);
        await _signInManager.SignInAsync(user, request.Remember);
        
        return Unit.Value;
    }

    private async Task<Domain.User> ConfirmUser(UserLoginQuery request)
    {
        var user = await GetUserByEmail(request);
        await CheckUserPassword(user, request.Password);

        return user;
    }

    private async Task<Domain.User> GetUserByEmail(UserLoginQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            throw new NotFoundException(nameof(Domain.User), request.Email);

        return user;
    }

    private async Task CheckUserPassword(Domain.User user, string password)
    {
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordCorrect)
            throw new AuthenticationException("Invalid password");
    }
}