using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common.Behaviors;
using Recommendations.Application.Common.Clouds.Algolia;
using Recommendations.Application.Common.Clouds.Firebase;
using Recommendations.Application.Common.ConnectionString;
using Recommendations.Application.Common.OAuthConfiguration;

namespace Recommendations.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAuthenticationConfiguration(configuration);
        services.AddConnectionStringConfiguration(configuration);
        services.AddValidationBehavior();
        services.AddAlgoliaService(configuration);
        services.AddFirebaseService(configuration);
    }

    private static void AddAuthenticationConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddGoogleOAuthConfiguration(configuration)
            .AddSpotifyOAuthConfiguration(configuration);
    }
}