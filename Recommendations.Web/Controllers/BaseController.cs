using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Web.Filters;

namespace Recommendations.Web.Controllers;

[ServiceFilter(typeof(UserAccessStatusValidationFilter))]
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

    protected Guid CurrentUserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : Guid.Empty;
    protected string Role => User.FindFirstValue(ClaimTypes.Role)!;
}
