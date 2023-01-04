using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Rating.Commands.Set;
using Recommendations.Web.Models.Rating;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/ratings")]
public class RatingController : BaseController
{
    public RatingController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper) { }

    [HttpPost]
    public async Task<ActionResult> SetRating([FromBody] SetRatingDto dto)
    {
        var setRatingCommand = _mapper.Map<SetRatingCommand>(dto);
        setRatingCommand.UserId = UserId;
        await _mediator.Send(setRatingCommand);
        
        return Ok();
    }
}