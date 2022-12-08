using Microsoft.Extensions.Configuration;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.Common;

public class ConnectionStringConfiguration : IConnectionStringConfiguration
{
    private readonly IConfiguration _configuration;

    public ConnectionStringConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetConnectionString()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var connectionString = environment == "Production"
            ? Environment.GetEnvironmentVariable("ProductionDbString")
            : _configuration["RecommendationsDevelop"];

        return connectionString;
    }
}