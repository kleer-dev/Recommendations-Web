using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Recommendations.Persistence.DbContexts;

namespace Recommendations.Persistence.DesignFactory;

public class RecommendationsDbContextFactory : IDesignTimeDbContextFactory<RecommendationsDbContext>
{
    public RecommendationsDbContext CreateDbContext(string[] args)
    {
        const string developConnectionString = "Host=localhost;Port=5432;Database=recommendations;" +
                                               "Username=postgres;Password=1234";
        const string productionConnectionString = "Host=dpg-ce6kauen6mpk2bmitd70-a;" +
                                                  "Port=5432;Database=recommendations_ipdc;" +
                                                  "Username=ivan;Password=KctXrR2FqUygeDT8I485RQVH9b9qhnah";

        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
            ? productionConnectionString
            : developConnectionString;
        
        var optionsBuilder = new DbContextOptionsBuilder<RecommendationsDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new RecommendationsDbContext(optionsBuilder.Options);
    }
}