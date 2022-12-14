using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Persistence.DbContexts;

namespace Recommendations.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        
        var connectionStringManager = serviceProvider
            .GetRequiredService<IConnectionStringConfiguration>();
        var connectionString = connectionStringManager
            .GetConnectionString();
        
        services.AddDbContext<RecommendationsDbContext>(options => 
            options.UseNpgsql(connectionString, o =>
                {
                    o.MigrationsAssembly(typeof(RecommendationsDbContext).Assembly.FullName);
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }));
        
        services.AddScoped<IRecommendationsDbContext, RecommendationsDbContext>();
        services.AddIdentityConfiguration();
    }
    
    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<Domain.User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = 
                    "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" +
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<RecommendationsDbContext>();
    }
}