using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Persistence.Initializers;

public class CategoriesInitializer
{
    private readonly IRecommendationsDbContext _context;

    private readonly string[] _categoriesName = { "Movies", "Games", "Books" };

    public CategoriesInitializer(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (await CheckExistence())
            return;

        var categories = _categoriesName
            .Select(categoryName => new Category { Name = categoryName })
            .ToList();
        
        await _context.Categories.AddRangeAsync(categories, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> CheckExistence()
    {
        return await _context.Categories
            .AnyAsync(c => _categoriesName.Contains(c.Name));
    }
}