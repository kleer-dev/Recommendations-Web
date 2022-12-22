using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Recommendations.Web.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ?? HttpContext.RequestServices.GetService<IMediator>()!;

    internal Guid? UserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : null;

    internal string? Role => User.FindFirstValue(ClaimTypes.Role);
}
