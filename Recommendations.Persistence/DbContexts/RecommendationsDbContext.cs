using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recommendations.Application.Common.Clouds.Algolia;
using Recommendations.Application.Interfaces;
using Recommendations.Domain;
using Recommendations.Persistence.EntityTypeConfigurations;

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
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Image> Images { get; set; }
    
    private readonly IServiceProvider _serviceProvider;

    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options,
        IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new ImageConfiguration());
        builder.ApplyConfiguration(new LikeConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new RatingConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        await _serviceProvider.GetRequiredService<AlgoliaDbSync>().Synchronize();
        return await base.SaveChangesAsync(cancellationToken);
    }
}