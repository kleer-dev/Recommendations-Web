using Microsoft.EntityFrameworkCore;
using Recommendations.Domain;

namespace Recommendations.Application.Common.Interfaces;

public interface IRecommendationsDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}