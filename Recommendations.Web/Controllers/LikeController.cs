using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Like.Commands.Set;
using Recommendations.Web.Models;
using Recommendations.Web.Models.Like;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/likes")]
public class LikeController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LikeController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> SetLike([FromBody] LikeDto dto)
    {
        var setLikeCommand = _mapper.Map<SetLikeCommand>(dto);
        setLikeCommand.UserId = UserId;
        await _mediator.Send(setLikeCommand);

        return Ok();
    }
}