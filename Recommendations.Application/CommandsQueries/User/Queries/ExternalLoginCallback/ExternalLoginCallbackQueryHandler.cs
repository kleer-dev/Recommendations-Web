using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Recommendations.Application.Common.Exceptions;

namespace Recommendations.Application.CommandsQueries.User.Queries.ExternalLoginCallback;

public class ExternalLoginCallbackQueryHandler
    : IRequestHandler<ExternalLoginCallbackQuery, Unit>
{
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;
    
    private const bool SaveCookiesAfterExitingBrowser = false;

    public ExternalLoginCallbackQueryHandler(SignInManager<Domain.User> signInManager,
        UserManager<Domain.User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ExternalLoginCallbackQuery request,
        CancellationToken cancellationToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();            
        var result = await _signInManager.ExternalLoginSignInAsync(info!.LoginProvider,
            info.ProviderKey, SaveCookiesAfterExitingBrowser, bypassTwoFactor: true);
        if (result.Succeeded) 
            return Unit.Value;
        
        var user = await GetUserByEmail(info) ?? await CreateUser(info);
        await _userManager.AddLoginAsync(user, info);
        await _signInManager.SignInAsync(user, SaveCookiesAfterExitingBrowser);
        
        return Unit.Value;
    }

    private async Task<Domain.User> CreateUser(ExternalLoginInfo info)
    {
        var newUser = new Domain.User
        {
            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        };
        var createResult = await _userManager.CreateAsync(newUser);
        if (!createResult.Succeeded)
            throw new AuthenticationException(createResult.Errors
                .Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));

        return newUser;
    }

    private async Task<Domain.User?> GetUserByEmail(ExternalLoginInfo info)
    {
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (email is null)
            throw new AuthenticationException("Email is empty");

        return await _userManager.FindByEmailAsync(email);
    }
}