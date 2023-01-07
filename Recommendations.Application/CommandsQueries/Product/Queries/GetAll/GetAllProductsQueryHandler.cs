using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Product.Queries.GetAll;

public class GetAllProductsQueryHandler
    : IRequestHandler<GetAllProductsQuery, GetAllProductsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllProductsVm> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .ProjectTo<GetAllProductsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllProductsVm {Products = products};
    }
}