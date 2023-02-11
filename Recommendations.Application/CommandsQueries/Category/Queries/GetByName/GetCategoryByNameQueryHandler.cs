using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Category.Queries.GetByName;

public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, Domain.Category>
{
    private readonly IRecommendationsDbContext _context;

    public GetCategoryByNameQueryHandler(IRecommendationsDbContext context) =>
        _context = context;

    public async Task<Domain.Category> Handle(GetCategoryByNameQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == request.Name, cancellationToken);
        if (category is null)
            throw new NotFoundException(nameof(Category), request.Name);

        return category;
    }
}