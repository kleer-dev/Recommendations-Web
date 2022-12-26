using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Recommendations.Application.Common.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Application.Common.Algolia;

public class AlgoliaDbSync
{
    private readonly IRecommendationsDbContext _context;
    private readonly IAlgoliaService _algoliaService;

    public AlgoliaDbSync(IRecommendationsDbContext context, IAlgoliaService algoliaService)
    {
        _context = context;
        _algoliaService = algoliaService;
    }

    public async Task Synchronize()
    {
        var trackedEntities = _context.ChangeTracker
            .Entries<Review>()
            .ToList();

        foreach (var entity in trackedEntities)
            await StartOperation(entity);
    }

    private async Task StartOperation(EntityEntry<Review> entityEntry)
    {
        var entity = entityEntry.Entity;
        await LoadAllEntityReferences(entity);
        
        switch (entityEntry.State)
        {
            case EntityState.Added or EntityState.Modified or EntityState.Unchanged:
                await _algoliaService.AddOrUpdateRecord(entity);
                break;
            case EntityState.Deleted:
                await _algoliaService.DeleteRecord(entity);
                break;
        }
    }

    private async Task LoadAllEntityReferences(Review review)
    {
        await _context.Reviews.Entry(review)
            .Reference(r => r.Category).LoadAsync();
        await _context.Reviews.Entry(review)
            .Collection(r => r.Comments).LoadAsync();
        await _context.Reviews.Entry(review)
            .Collection(r => r.Likes).LoadAsync();
        await _context.Reviews.Entry(review)
            .Collection(r => r.Tags).LoadAsync();
    }
}