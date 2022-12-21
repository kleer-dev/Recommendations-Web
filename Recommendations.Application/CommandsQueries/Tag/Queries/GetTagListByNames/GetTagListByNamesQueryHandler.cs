using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Tag.Queries.GetTagListByNames;

public class GetTagListByNamesQueryHandler
    : IRequestHandler<GetTagListByNamesQuery, IEnumerable<Domain.Tag>>
{
    private readonly IRecommendationsDbContext _context;

    public GetTagListByNamesQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Tag>> Handle(GetTagListByNamesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Tags
            .Where(t => request.Tags.Contains(t.Name))
            .ToListAsync(cancellationToken);
    }
}