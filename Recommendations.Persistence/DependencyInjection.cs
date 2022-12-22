using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Persistence.DbContexts;

namespace Recommendations.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContextConfiguration();
        services.AddIdentityConfiguration();
        services.AddCookieConfiguration();
    }

    private static void AddDbContextConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var connectionStringManager = serviceProvider
            .GetRequiredService<IConnectionStringConfiguration>();
        var connectionString = connectionStringManager
            .GetConnectionString();
        
        services.AddDbContext<RecommendationsDbContext>(options => 
            options.UseNpgsql(connectionString, o =>
            {
                o.MigrationsAssembly(typeof(RecommendationsDbContext).Assembly.FullName);
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));
        
        services.AddScoped<IRecommendationsDbContext, RecommendationsDbContext>();
    }
    
    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<Domain.User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = 
                    "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" +
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<RecommendationsDbContext>();
    }
    
    private static void AddCookieConfiguration(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 401;
                    return Task.FromResult(0);
                },
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 401;
                    return Task.FromResult(0);
                }
            };
        });
    }
}