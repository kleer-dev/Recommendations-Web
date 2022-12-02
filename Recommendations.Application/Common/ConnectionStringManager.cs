using Microsoft.Extensions.Configuration;

namespace Recommendations.Application.Common;

public class ConnectionStringManager
{
    private readonly IConfiguration _configuration;

    public ConnectionStringManager(IConfiguration configuration)
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