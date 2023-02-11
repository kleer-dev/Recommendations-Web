using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Constants;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Commands.Registration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Guid>
{
    private readonly IRecommendationsDbContext _context;
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IMapper _mapper;

    public UserRegistrationCommandHandler(IRecommendationsDbContext context,
        UserManager<Domain.User> userManager,
        SignInManager<Domain.User> signInManager, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var isUserExist = await CheckUserExistence(request, cancellationToken);
        if (isUserExist)
            throw new RecordIsExistException(typeof(Domain.User));
        
        var user = _mapper.Map<Domain.User>(request);
        await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, Roles.User);
        await _signInManager.SignInAsync(user, request.Remember);

        return user.Id;
    }

    private async Task<bool> CheckUserExistence(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == request.Email ||
                u.UserName == request.Login, cancellationToken);
    }
}