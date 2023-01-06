using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Comment.Commands;
using Recommendations.Application.CommandsQueries.Comment.Queries.GetAll;
using Recommendations.Web.Models.Comment;

namespace Recommendations.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/comments")]
public class CommentController : BaseController
{
    public CommentController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper) { }

    [HttpPost]
    public async Task<ActionResult<Guid>> Send([FromBody] CreateCommentDto dto)
    {
        var createCommentCommand = _mapper.Map<CreateCommentCommand>(dto);
        createCommentCommand.UserId = CurrentUserId;
        var commentId = await _mediator.Send(createCommentCommand);
        
        return Ok(commentId);
    }
    
    [AllowAnonymous]
    [HttpGet("{reviewId:guid}")]
    public async Task<ActionResult<IEnumerable<GetAllCommentsDto>>> GetAll(Guid reviewId)
    {
        var getAllCommentsQuery = new GetAllCommentsQuery(reviewId);
        var commentsVm = await _mediator.Send(getAllCommentsQuery);
        
        return Ok(commentsVm.Comments);
    }
}