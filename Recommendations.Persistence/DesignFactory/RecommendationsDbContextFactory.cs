using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Recommendations.Persistence.DbContexts;

namespace Recommendations.Persistence.DesignFactory;

public class RecommendationsDbContextFactory : IDesignTimeDbContextFactory<RecommendationsDbContext>
{
    public RecommendationsDbContext CreateDbContext(string[] args)
    {
        var connectionString = GetConnectionString();
        var optionsBuilder = new DbContextOptionsBuilder<RecommendationsDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new RecommendationsDbContext(optionsBuilder.Options);
    }

    private static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
            ? "Host=dpg-ce6kauen6mpk2bmitd70-a;" +
              "Port=5432;Database=recommendations_ipdc;" +
              "Username=ivan;Password=KctXrR2FqUygeDT8I485RQVH9b9qhnah"
            :  "Host=localhost;Port=5432;Database=recommendations;" +
               "Username=postgres;Password=1234";
        
        return connectionString
               ?? throw new NullReferenceException("The connection is empty");
    }
}