using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Comment.Queries.GetAll;

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, GetAllCommentsVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAllCommentsQueryHandler(IRecommendationsDbContext context,
        IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<GetAllCommentsVm> Handle(GetAllCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var comments = await _context.Comments
            .Include(c => c.Review)
            .Include(c => c.User)
            .Where(c => c.Review.Id == request.ReviewId)
            .OrderBy(c => c.CreationDate)
            .ProjectTo<GetAllCommentsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetAllCommentsVm { Comments = comments };
    }
}