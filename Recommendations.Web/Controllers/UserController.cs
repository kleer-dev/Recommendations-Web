using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.ExternalAuthentication.Queries.GetAuthenticationProperties;
using Recommendations.Application.CommandsQueries.User.Commands.Block;
using Recommendations.Application.CommandsQueries.User.Commands.Delete;
using Recommendations.Application.CommandsQueries.User.Commands.Registration;
using Recommendations.Application.CommandsQueries.User.Commands.SetRole;
using Recommendations.Application.CommandsQueries.User.Commands.Unblock;
using Recommendations.Application.CommandsQueries.User.Queries;
using Recommendations.Application.CommandsQueries.User.Queries.ExternalLoginCallback;
using Recommendations.Application.CommandsQueries.User.Queries.GetAllUsers;
using Recommendations.Application.CommandsQueries.User.Queries.GetUserInfo;
using Recommendations.Application.CommandsQueries.User.Queries.Login;
using Recommendations.Application.Common.Constants;
using Recommendations.Domain;
using Recommendations.Web.Filters;
using Recommendations.Web.Models.User;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly SignInManager<User> _signInManager;

    public UserController(IMediator mediator, IMapper mapper,
        SignInManager<User> signInManager) : base(mediator, mapper)
    {
        _signInManager = signInManager;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("get-all-users")]
    public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var usersVm = await Mediator.Send(getAllUsersQuery);

        return usersVm.Users.ToList();
    }

    [Authorize]
    [HttpGet("get-info")]
    public async Task<ActionResult<GetUserDto>> GetInfo()
    {
        var getUserInfoQuery = new GetUserInfoQuery(CurrentUserId);
        var userInfo = await Mediator.Send(getUserInfoQuery);

        return Ok(userInfo);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("get-info/{userId:guid}")]
    public async Task<ActionResult<GetUserDto>> GetInfo(Guid userId)
    {
        var getUserInfoQuery = new GetUserInfoQuery(userId);
        var userInfo = await Mediator.Send(getUserInfoQuery);

        return Ok(userInfo);
    }

    [AllowAnonymous]
    [HttpGet("get-role")]
    public ActionResult<RoleDto> GetCurrentUserRole()
    {
        return Ok(new RoleDto(Role));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("set-role")]
    public async Task<ActionResult> SetRole(SetRoleDto dto)
    {
        var setUserRoleCommand = Mapper.Map<SetUserRoleCommand>(dto);
        await Mediator.Send(setUserRoleCommand);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("check-auth")]
    public ActionResult<bool> GetUserAuthStatus()
    {
        return Ok(User.Identity!.IsAuthenticated);
    }
    
    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<ActionResult> Registration([FromBody] UserRegistrationDto dto)
    {
        var registrationCommand = Mapper.Map<UserRegistrationCommand>(dto);
        var userId = await Mediator.Send(registrationCommand);

        return Created("api/users/registration", userId);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        var loginQuery = Mapper.Map<UserLoginQuery>(dto);
        await Mediator.Send(loginQuery);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("external-login")]
    public async Task<ActionResult> ExternalLogin(string provider)
    {
        var getAuthenticationPropertiesQuery
            = new GetAuthenticationPropertiesQuery(provider, "/login-callback");
        var authenticationProperties =
            await Mediator.Send(getAuthenticationPropertiesQuery);

        return Challenge(authenticationProperties, provider);
    }

    [AllowAnonymous]
    [HttpGet("external-login-callback")]
    public async Task<ActionResult> HandleExternalLogin()
    {
        var externalLoginCallbackQuery = new ExternalLoginCallbackQuery();
        await Mediator.Send(externalLoginCallbackQuery);

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        var deleteUserCommand = new DeleteUserCommand(userId);
        await Mediator.Send(deleteUserCommand);

        return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("block/{userId:guid}")]
    public async Task<ActionResult> BlockUser(Guid userId)
    {
        var blockUserCommand = new BlockUserCommand(userId);
        await Mediator.Send(blockUserCommand);

        return Ok();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("unblock/{userId:guid}")]
    public async Task<ActionResult> UnblockUser(Guid userId)
    {
        var unblockUserCommand = new UnblockUserCommand(userId);
        await Mediator.Send(unblockUserCommand);

        return Ok();
    }
}