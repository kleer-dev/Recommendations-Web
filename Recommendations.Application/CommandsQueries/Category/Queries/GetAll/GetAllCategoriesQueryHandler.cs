using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Category.Queries.GetAll;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllCategoriesVm> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .ProjectTo<GetAllCategoriesDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllCategoriesVm { Categories = categories };
    }
}