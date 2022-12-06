using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Persistence.DbContexts;

public sealed class RecommendationsDbContext : IdentityDbContext<User>,
    IRecommendationsDbContext
{
    public DbSet<User> Users { get; set; }

    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}