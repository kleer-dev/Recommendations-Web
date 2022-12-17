using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.ExternalAuthentication.Queries.GetAuthenticationProperties;
using Recommendations.Application.CommandsQueries.User.Commands.Registration;
using Recommendations.Application.CommandsQueries.User.Queries.ExternalLoginCallback;
using Recommendations.Application.CommandsQueries.User.Queries.Login;
using Recommendations.Domain;
using Recommendations.Web.Models;
using Recommendations.Web.Models.User;

namespace Recommendations.Web.Controllers;

[ApiController]
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

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] UserRegistrationDto dto)
    {
        var registrationCommand = _mapper.Map<UserRegistrationCommand>(dto);
        await _mediator.Send(registrationCommand);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var loginQuery = _mapper.Map<UserLoginQuery>(dto);
        await _mediator.Send(loginQuery);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("external-login")]
    public async Task<IActionResult> ExternalLogin(string provider)
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
    
    [HttpGet("external-login-callback")]
    public async Task<IActionResult> HandleExternalLogin()
    {
        var externalLoginCallbackQuery = new ExternalLoginCallbackQuery();
        await _mediator.Send(externalLoginCallbackQuery);
        
        return Ok();                        
    }
    
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();                        
    }
}