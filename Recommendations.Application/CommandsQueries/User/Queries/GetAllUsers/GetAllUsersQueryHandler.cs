using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, GetAllUsersVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAllUsersVm> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .ProjectTo<GetUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new GetAllUsersVm { Users = users };
    }
}