using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Rating.Commands.Set;
using Recommendations.Web.Models.Rating;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/ratings")]
public class RatingController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RatingController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SetRating([FromBody] SetRatingDto dto)
    {
        var setRatingCommand = _mapper.Map<SetRatingCommand>(dto);
        setRatingCommand.UserId = UserId;
        await _mediator.Send(setRatingCommand);

        return Ok();
    }
}