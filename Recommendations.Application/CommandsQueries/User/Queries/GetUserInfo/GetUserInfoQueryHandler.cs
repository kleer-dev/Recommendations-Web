using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserDto>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;

    public GetUserInfoQueryHandler(IRecommendationsDbContext context,
        IMapper mapper, UserManager<Domain.User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<GetUserDto> Handle(GetUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
            throw new NotFoundException("The user not found");
        var getUserDto = _mapper.Map<GetUserDto>(user);
        getUserDto.Role = await GetUserRole(user);
        
        return getUserDto;
    }
    
    private async Task<string> GetUserRole(Domain.User user)
    {
        var role = await _userManager.GetRolesAsync(user);
        return role.FirstOrDefault() ?? Roles.User;
    }
}