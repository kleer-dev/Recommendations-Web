using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Tag.Queries.GetAll;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/tags")]
public class TagController : BaseController
{
    public TagController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllTagsDto>>> GetAll()
    {
        var getAllTagsQuery = new GetAllTagsQuery();
        var allTagsVm = await _mediator.Send(getAllTagsQuery);
        
        return Ok(allTagsVm.Tags);
    }
}