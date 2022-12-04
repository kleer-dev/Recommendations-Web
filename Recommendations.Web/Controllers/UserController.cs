using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.User.Commands.Registration;
using Recommendations.Application.CommandsQueries.User.Queries.Login;
using Recommendations.Web.Models;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] UserRegistrationDto dto)
    {
        var registrationCommand = _mapper.Map<UserRegistrationCommand>(dto);
        await _mediator.Send(registrationCommand);
        
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var loginQuery = _mapper.Map<UserLoginQuery>(dto);
        await _mediator.Send(loginQuery);
        
        return Ok();
    }
}