using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;
using Recommendations.Domain;

namespace Recommendations.Persistence.Initializers;

public class CategoriesInitializer
{
    private readonly IRecommendationsDbContext _context;

    private readonly string[] _categoriesName =
    {
        "Movies", "Games", "Books", "Cars", "Devices", "Software"
    };

    public CategoriesInitializer(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var allCategories = await _context.Categories
            .Select(c => c.Name)
            .ToListAsync(cancellationToken);
        var newCategories = _categoriesName
            .Where(newCategory => allCategories
                .All(category => category != newCategory))
            .Select(categoryName => new Category { Name = categoryName })
            .ToList();

        await _context.Categories.AddRangeAsync(newCategories, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}