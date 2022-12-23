using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.ExternalAuthentication.Queries.GetAuthenticationProperties;
using Recommendations.Application.CommandsQueries.User.Commands.Registration;
using Recommendations.Application.CommandsQueries.User.Queries.ExternalLoginCallback;
using Recommendations.Application.CommandsQueries.User.Queries.GetAllUsers;
using Recommendations.Application.CommandsQueries.User.Queries.Login;
using Recommendations.Application.Common.Constants;
using Recommendations.Domain;
using Recommendations.Web.Models.User;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;

    public UserController(IMapper mapper, IMediator mediator,
        SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _mediator = mediator;
        _signInManager = signInManager;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("get-all-users")]
    public async Task<ActionResult<IEnumerable<GetAllUsersDto>>> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var usersVm = await _mediator.Send(getAllUsersQuery);

        return usersVm.Users.ToList();
    }

    [AllowAnonymous]
    [HttpGet("get-role")]
    public ActionResult<RoleDto> GetRole()
    {
        var roleDto = new RoleDto { RoleName = Role };
        return Ok(roleDto);
    }

    [HttpGet("check-auth")]
    public ActionResult<bool> CheckAuth() =>
        Ok(User.Identity.IsAuthenticated);

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<ActionResult> Registration([FromBody] UserRegistrationDto dto)
    {
        var registrationCommand = _mapper.Map<UserRegistrationCommand>(dto);
        var userId = await _mediator.Send(registrationCommand);

        return Created("api/users/registration", userId);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        var loginQuery = _mapper.Map<UserLoginQuery>(dto);
        await _mediator.Send(loginQuery);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("external-login")]
    public async Task<ActionResult> ExternalLogin(string provider)
    {
        var getAuthenticationPropertiesQuery = new GetAuthenticationPropertiesQuery
        {
            Path = "/login-callback",
            Provider = provider
        };
        var authenticationProperties =
            await _mediator.Send(getAuthenticationPropertiesQuery);

        return Challenge(authenticationProperties, provider);
    }

    [AllowAnonymous]
    [HttpGet("external-login-callback")]
    public async Task<ActionResult> HandleExternalLogin()
    {
        var externalLoginCallbackQuery = new ExternalLoginCallbackQuery();
        await _mediator.Send(externalLoginCallbackQuery);

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}