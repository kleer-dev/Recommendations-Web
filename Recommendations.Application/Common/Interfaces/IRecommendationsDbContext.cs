namespace Recommendations.Application.Common.Interfaces;

public interface IRecommendationsDbContext
{

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}