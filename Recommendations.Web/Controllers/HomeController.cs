using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.User.Queries.ExternalLoginCallback;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/home")]
public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("check-auth")]
    public IActionResult IsAuthenticated()
    {
        return new ObjectResult(User.Identity.IsAuthenticated);
    }
    
    [Authorize]
    [HttpGet("name")]
    public IActionResult Name()
    {
        var givenName = User.FindFirstValue(ClaimTypes.Email);
        return Ok(givenName);
    }
    
    [AllowAnonymous]
    [HttpGet("response")]
    public async Task<IActionResult> ExternalLoginHandler()
    {
        var externalLoginCallbackQuery = new ExternalLoginCallbackQuery();
        await _mediator.Send(externalLoginCallbackQuery);
    
        return Ok();                        
    }
}