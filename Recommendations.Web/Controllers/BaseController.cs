using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Recommendations.Web.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;   
    protected readonly IMapper _mapper;

    public BaseController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    protected Guid? UserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : null;

    protected string Role => User.FindFirstValue(ClaimTypes.Role)!;
}
