using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Constants;

namespace Recommendations.Application.CommandsQueries.User.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;
    private readonly IMediator _mediator;

    public GetUserInfoQueryHandler(IMapper mapper,
        UserManager<Domain.User> userManager, IMediator mediator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<GetUserDto> Handle(GetUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await GetUser(request.UserId, cancellationToken);
        var getUserDto = _mapper.Map<GetUserDto>(user);
        getUserDto.Role = await GetUserRole(user);
        
        return getUserDto;
    }

    private async Task<Domain.User> GetUser(Guid userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery, cancellationToken);
    }
    
    private async Task<string> GetUserRole(Domain.User user)
    {
        var role = await _userManager.GetRolesAsync(user);
        return role.FirstOrDefault() ?? Roles.User;
    }
}