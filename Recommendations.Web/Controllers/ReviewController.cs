using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Review.Commands.Create;
using Recommendations.Application.CommandsQueries.Review.Commands.Delete;
using Recommendations.Application.CommandsQueries.Review.Commands.Update;
using Recommendations.Application.CommandsQueries.Review.Queries;
using Recommendations.Application.CommandsQueries.Review.Queries.GetDto;
using Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByParam;
using Recommendations.Application.CommandsQueries.Review.Queries.GetReviewsByUserId;
using Recommendations.Application.CommandsQueries.Review.Queries.GetUpdate;
using Recommendations.Application.Common.Constants;
using Recommendations.Web.Models.Review;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/reviews")]
public class ReviewController : BaseController
{
    public ReviewController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper) { }

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<GetAllReviewsDto>>> GetAllReviews(string? filtrate,
        int? count, string? tag)
    {
        var getReviewsByParam = new GetReviewsByParamQuery(count, filtrate, tag);
        var getReviewsByParamVm = await _mediator.Send(getReviewsByParam);
        var reviews = getReviewsByParamVm.Reviews.ToList();
        
        return Ok(reviews);
    }

    [AllowAnonymous]
    [HttpGet("{reviewId:guid}")]
    public async Task<ActionResult<GetReviewDto>> Get(Guid reviewId)
    {
        var getReviewQuery = new GetReviewDtoQuery(reviewId, UserId);
        var review = await _mediator.Send(getReviewQuery);
        
        return Ok(review);
    }
    
    [HttpGet("get-by-user")]
    public async Task<ActionResult<IEnumerable<GetReviewsByUserIdDto>>> GetReviewsByUser()
    {
        var getReviewsByUserIdQuery = new GetReviewsByUserIdQuery(UserId);
        var reviewsVm = await _mediator.Send(getReviewsByUserIdQuery);
        
        return Ok(reviewsVm.Reviews);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("get-by-user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<GetReviewsByUserIdDto>>> GetReviewsByUser(Guid userId)
    {
        var getReviewsByUserIdQuery = new GetReviewsByUserIdQuery(userId);
        var reviewsVm = await _mediator.Send(getReviewsByUserIdQuery);
        
        return Ok(reviewsVm.Reviews);
    }

    [HttpGet("get-update-review/{reviewId:guid}")]
    public async Task<ActionResult> GetUpdateReview(Guid reviewId)
    {
        var getUpdateReviewQuery = new GetUpdateReviewQuery(reviewId);
        var review = await _mediator.Send(getUpdateReviewQuery);
        
        return Ok(review);
    }

    [HttpPost, DisableRequestSizeLimit]
    public async Task<ActionResult> Create([FromForm] CreateReviewDto dto)
    {
        var createReviewCommand = _mapper.Map<CreateReviewCommand>(dto);
        createReviewCommand.UserId = UserId;
        var reviewId = await _mediator.Send(createReviewCommand);
        
        return Created("api/reviews", reviewId);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{userId:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> Create(Guid userId, [FromForm] CreateReviewDto dto)
    {
        var createReviewCommand = _mapper.Map<CreateReviewCommand>(dto);
        createReviewCommand.UserId = userId;
        var reviewId = await _mediator.Send(createReviewCommand);
        
        return Created("api/reviews", reviewId);
    }

    [HttpPut, DisableRequestSizeLimit]
    public async Task<ActionResult> Update([FromForm] UpdateReviewDto reviewDto)
    {
        var updateReviewCommand = _mapper.Map<UpdateReviewCommand>(reviewDto);
        await _mediator.Send(updateReviewCommand);
        
        return NoContent();
    }

    [HttpDelete("{reviewId:guid}")]
    public async Task<ActionResult> Delete(Guid reviewId)
    {
        var deleteReviewCommand = new DeleteReviewCommand(reviewId);
        await _mediator.Send(deleteReviewCommand);
        
        return NoContent();
    }
}