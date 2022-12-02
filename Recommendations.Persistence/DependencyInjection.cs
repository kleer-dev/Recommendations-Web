using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common;
using Recommendations.Application.Common.Interfaces;
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
        
        services.AddDbContext<RecommendationsDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IRecommendationsDbContext, RecommendationsDbContext>();
        
        return services;
    }
    
    
}