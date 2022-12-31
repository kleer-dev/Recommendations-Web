using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Queries.GetUserInfo;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserDto>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetUserInfoQueryHandler(IRecommendationsDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUserDto> Handle(GetUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        if (request.UserId is null)
            throw new NullReferenceException("The user id is null");
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
            throw new NullReferenceException($"The user with id:{request.UserId} not found");
        return _mapper.Map<GetUserDto>(user);
    }
}