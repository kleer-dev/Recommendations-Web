using Microsoft.EntityFrameworkCore;
using Recommendations.Domain;

namespace Recommendations.Application.Common.Interfaces;

public interface IRecommendationsDbContext
{
    public DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}