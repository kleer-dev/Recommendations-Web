using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Interfaces;
using Recommendations.Domain;
using Recommendations.Persistence.DbContexts;
using Recommendations.Persistence.DesignFactory;
using Recommendations.Persistence.Initializers;

namespace Recommendations.Persistence;

public static class DependencyInjection
{
    public static async Task AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextConfiguration();
        services.AddIdentityConfiguration();
        services.AddCookieConfiguration();
        await services.AddDbInitializers(configuration);
    }

    private static void AddDbContextConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var connectionStringManager = serviceProvider
            .GetRequiredService<IConnectionStringConfiguration>();
        var connectionString = connectionStringManager
            .GetConnectionString();

        services.AddDbContext<RecommendationsDbContext>(options =>
        {
            options.UseNpgsql(connectionString, o =>
            {
                o.MigrationsAssembly(typeof(RecommendationsDbContext).Assembly.FullName);
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        } );
        
        services.AddScoped<IRecommendationsDbContext, RecommendationsDbContext>();
    }

    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = null!;
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
        var redirectBehaviorOverride = (RedirectContext<CookieAuthenticationOptions> context,
            int statusCode) =>
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            return Task.FromResult(0);
        };

        services.ConfigureApplicationCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context => redirectBehaviorOverride.Invoke(context, 401),
                OnRedirectToAccessDenied = context => redirectBehaviorOverride.Invoke(context, 403)
            };
        });
    }

    private static async Task AddDbInitializers(this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var userManager = serviceProvider
            .GetRequiredService<UserManager<User>>();
        var rolesManager = serviceProvider
            .GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var recommendationsDbContext = serviceProvider
            .GetRequiredService<IRecommendationsDbContext>();

        await new RoleInitializer(rolesManager).InitializeAsync();
        await new AdminInitializer(userManager, configuration).InitializeAsync();
        await new CategoriesInitializer(recommendationsDbContext).InitializeAsync(new CancellationToken());
    }
}