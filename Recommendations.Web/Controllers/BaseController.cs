using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Web.Filters;

namespace Recommendations.Web.Controllers;

[ServiceFilter(typeof(UserAccessStatusValidationFilter))]
[ServiceFilter(typeof(AdminRoleValidationFilter))]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;   
    protected readonly IMapper Mapper;

    public BaseController(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    protected Guid CurrentUserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : Guid.Empty;
    protected string Role => User.FindFirstValue(ClaimTypes.Role)!;
}
