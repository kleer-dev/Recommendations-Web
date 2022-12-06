using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common;

namespace Recommendations.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAuth(configuration);
        services.AddConnectionStringsManager(configuration);
        
        return services;
    }

    private static IServiceCollection AddConnectionStringsManager(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringManager = new ConnectionStringManager(configuration);
        services.AddSingleton(connectionStringManager);

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
            })
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Google:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Google:ClientSecret"] ?? string.Empty;
            })
            .AddSpotify(options =>
            {
                options.ClientId = configuration["Spotify:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Spotify:ClientSecret"] ?? string.Empty;
                options.Scope.Add("user-read-email");
            });

        return services;
    }
}
