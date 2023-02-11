using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Recommendations.Persistence.DbContexts;

namespace Recommendations.Persistence.DesignFactory;

public class RecommendationsDbContextFactory
    : IDesignTimeDbContextFactory<RecommendationsDbContext>
{
    private readonly IServiceProvider _serviceProvider;

    private const string PersistenceAssembly = "Recommendations.Persistence";
    private const string MainAssembly = "Recommendations.Web";

    public RecommendationsDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public RecommendationsDbContextFactory() { }

    public RecommendationsDbContext CreateDbContext(string[] args)
    {
        var connectionString = GetConnectionString();
        var optionsBuilder = new DbContextOptionsBuilder<RecommendationsDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new RecommendationsDbContext(optionsBuilder.Options, _serviceProvider);
    }

    private static string GetConnectionString()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!
            .Replace(PersistenceAssembly, MainAssembly);
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();
        var configuration = builder.Build();
        
        var connectionString = environment == "Production"
            ? configuration["ProductionDbString"]
            : configuration["RecommendationsDevelop"];
        if (connectionString is null)
            throw new NullReferenceException("The connections string is missing");

        return connectionString;
    }
}