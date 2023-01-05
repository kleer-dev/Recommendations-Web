using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, GetAllUsersVm>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;

    public GetAllUsersQueryHandler(IRecommendationsDbContext context,
        IMapper mapper, UserManager<Domain.User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<GetAllUsersVm> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var usersDto = new List<GetUserDto>();
        var users = await _context.Users
            .ToListAsync(cancellationToken);

        foreach (var user in users)
        {
            var userDto = _mapper.Map<GetUserDto>(user);
            userDto.Role = await GetUserRole(user);
            usersDto.Add(userDto);
        }
        
        return new GetAllUsersVm { Users = usersDto };
    }

    private async Task<string> GetUserRole(Domain.User user)
    {
        var role = await _userManager.GetRolesAsync(user);
        return role.FirstOrDefault() ?? Roles.User;
    }
}