using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Tag.Queries.GetAll;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/tags")]
public class TagController : BaseController
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllTagsDto>> GetAll()
    {
        var getAllTagsQuery = new GetAllTagsQuery();
        var allTagsVm = await _mediator.Send(getAllTagsQuery);
        
        return Ok(allTagsVm.Tags);
    }
}