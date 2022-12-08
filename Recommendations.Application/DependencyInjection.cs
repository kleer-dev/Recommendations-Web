using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common;
using Recommendations.Application.Common.Clouds;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAuthenticationConfiguration(configuration);
        services.AddConnectionStringsManager(configuration);
        services.AddMegaCloudConfiguration(configuration);
    }

    private static void AddConnectionStringsManager(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>(_
            => new ConnectionStringConfiguration(configuration));
    }

    private static void AddMegaCloudConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var email = configuration["MegaCloud:Email"];
        var password = configuration["MegaCloud:Password"];

        if (email is null || password is null)
            throw new NullReferenceException("Missing data for Mega Cloud");

        services.AddScoped<IMegaCloudClient, MegaCloudClient>(_ =>
            new MegaCloudClient(email, password));
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
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Google:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Google:ClientSecret"] ?? string.Empty;
                options.SignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddSpotify(options =>
            {
                options.ClientId = configuration["Spotify:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Spotify:ClientSecret"] ?? string.Empty;
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.Scope.Add("user-read-email");
            });
    }
}