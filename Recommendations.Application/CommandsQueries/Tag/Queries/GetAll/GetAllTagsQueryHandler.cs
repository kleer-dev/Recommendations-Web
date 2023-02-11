using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Tag.Queries.GetAll;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, GetAllTagsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTagsQueryHandler(IRecommendationsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllTagsVm> Handle(GetAllTagsQuery request,
        CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
            .ProjectTo<GetAllTagsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllTagsVm { Tags = tags };
    }
}