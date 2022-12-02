using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
    
    public static IServiceCollection AddConnectionStringsManager(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringManager = new ConnectionStringManager(configuration);
        services.AddSingleton(connectionStringManager);

        return services;
    }
}
