using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Persistence.DbContexts;

public sealed class RecommendationsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>,
    IRecommendationsDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Like> Likes { get; set; }

    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}