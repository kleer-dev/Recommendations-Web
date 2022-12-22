using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/home")]
public class HomeController : BaseController
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }
}