using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;
using Recommendations.Persistence.DbContexts;

namespace Recommendations.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        
        var connectionStringManager = serviceProvider
            .GetRequiredService<ConnectionStringManager>();
        var connectionString = connectionStringManager
            .GetConnectionString();
        
        services.AddDbContext<RecommendationsDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IRecommendationsDbContext, RecommendationsDbContext>();
        services.AddIdentityConfiguration();
        
        return services;
    }
    
    private static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<RecommendationsDbContext>();

        return services;
    }
}