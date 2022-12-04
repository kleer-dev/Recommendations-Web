using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Commands.Registration;

public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Unit>
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

    public async Task<Unit> Handle(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        var isUserExist = await CheckUserExistence(request, cancellationToken);
        if (isUserExist)
            throw new RecordIsExistException(typeof(Domain.User));

        var user = _mapper.Map<Domain.User>(request);
        await _userManager.CreateAsync(user, request.Password);
        await _signInManager.SignInAsync(user, request.Remember);

        return Unit.Value;
    }

    private async Task<bool> CheckUserExistence(UserRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == request.Email ||
                u.UserName == request.Login, cancellationToken);
    }
}