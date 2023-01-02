using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Recommendations.Application.CommandsQueries.ExternalAuthentication.Queries.GetAuthenticationProperties;

public class GetAuthenticationPropertiesQuery : IRequest<AuthenticationProperties>
{
    public string? Provider { get; set; }
    public string? Path { get; set; }

    public GetAuthenticationPropertiesQuery(string? provider, string? path)
    {
        Provider = provider;
        Path = path;
    }
}