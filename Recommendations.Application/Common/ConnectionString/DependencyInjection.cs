using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.Common.ConnectionString;

public static class DependencyInjection
{
    public static void AddConnectionStringConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>(_
            => new ConnectionStringConfiguration(configuration));
    }
}