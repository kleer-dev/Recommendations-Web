using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Recommendations.Application.CommandsQueries.ExternalAuthentication.Queries.GetAuthenticationProperties;

public class GetAuthenticationPropertiesQueryHandler
    : IRequestHandler<GetAuthenticationPropertiesQuery, AuthenticationProperties>
{
    private readonly SignInManager<Domain.User> _signInManager;

    public GetAuthenticationPropertiesQueryHandler(SignInManager<Domain.User> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<AuthenticationProperties> Handle(GetAuthenticationPropertiesQuery request,
        CancellationToken cancellationToken)
    {
        var authenticationProperties = _signInManager
            .ConfigureExternalAuthenticationProperties(request.Provider, request.Path);

        return Task.FromResult(authenticationProperties);
    }
}