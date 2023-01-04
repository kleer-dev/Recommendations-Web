using Microsoft.Extensions.Configuration;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.Common.ConnectionString;

public class ConnectionStringConfiguration : IConnectionStringConfiguration
{
    private readonly IConfiguration _configuration;

    public ConnectionStringConfiguration(IConfiguration configuration) =>
        _configuration = configuration;

    public string GetConnectionString()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var connectionString = environment == "Production"
            ? Environment.GetEnvironmentVariable("ProductionDbString")
            : _configuration["RecommendationsDevelop"];

        if (connectionString is null)
            throw new NullReferenceException("The connection is empty");

        return connectionString;
    }
}