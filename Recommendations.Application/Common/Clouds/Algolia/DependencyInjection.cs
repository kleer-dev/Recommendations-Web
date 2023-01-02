using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.Common.Clouds.Algolia;

public static class DependencyInjection
{
    public static void AddAlgoliaService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAlgolia(configuration);
        services.AddScoped<AlgoliaDbSync>();
    }
    
    private static void AddAlgolia(this IServiceCollection services,
        IConfiguration configuration)
    {
        var applicationId = configuration["Algolia:ApplicationId"];
        var adminKey = configuration["Algolia:AdminKey"];
        var indexName = configuration["Algolia:IndexName"];

        if (applicationId is null || adminKey is null || indexName is null)
            throw new NullReferenceException("Missing keys for Algolia");

        services.AddSingleton<IAlgoliaService, AlgoliaService>(_ => 
            new AlgoliaService(applicationId, adminKey, indexName));
    }
}