using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendations.Application.CommandsQueries.Comment.Commands;
using Recommendations.Application.CommandsQueries.Comment.Queries.GetAll;
using Recommendations.Web.Models.Comment;

namespace Recommendations.Web.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CommentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Send([FromBody] CreateCommentDto dto)
    {
        var createCommentCommand = _mapper.Map<CreateCommentCommand>(dto);
        createCommentCommand.UserId = UserId;
        var commentId = await _mediator.Send(createCommentCommand);

        return Ok(commentId);
    }

    [HttpGet("{reviewId:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetAllCommentsDto>>> GetAll(Guid reviewId)
    {
        var getAllCommentsQuery = new GetAllCommentsQuery
        {
            ReviewId = reviewId
        };
        var commentsVm = await _mediator.Send(getAllCommentsQuery);

        return Ok(commentsVm.Comments);
    }
}