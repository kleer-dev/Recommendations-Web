using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Recommendations.Application.Common.OAuthConfiguration;

public static class SpotifyOAuthConfiguration
{
    public static AuthenticationBuilder AddSpotifyOAuthConfiguration(
        this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
    {
        authenticationBuilder.AddSpotify(options =>
        {
            options.ClientId = configuration["Spotify:ClientId"] ?? string.Empty;
            options.ClientSecret = configuration["Spotify:ClientSecret"] ?? string.Empty;
            options.SignInScheme = IdentityConstants.ExternalScheme;
            options.Scope.Add("user-read-email");
        });

        return authenticationBuilder;
    }
}