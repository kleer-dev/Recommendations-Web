using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Review.Commands.Create;
using Recommendations.Web.Models;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/reviews")]
public class ReviewController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReviewController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateReviewDto dto)
    {
        var createReviewCommand = _mapper.Map<CreateReviewCommand>(dto);
        createReviewCommand.UserId = UserId;
        var reviewId = await _mediator.Send(createReviewCommand);
        
        return Ok(reviewId);
    }
}