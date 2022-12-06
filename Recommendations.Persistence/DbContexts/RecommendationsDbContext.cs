using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Persistence.DbContexts;

public class RecommendationsDbContext : IdentityDbContext<User>,
    IRecommendationsDbContext
{
    public DbSet<User> Users { get; set; }

    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}