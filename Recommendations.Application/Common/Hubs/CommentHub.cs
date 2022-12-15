using MediatR;
using Microsoft.AspNetCore.SignalR;
using Recommendations.Application.CommandsQueries.Comment.Queries.Get;

namespace Recommendations.Application.Common.Hubs;

public class CommentHub : Hub
{
   private readonly IMediator _mediator;

   public CommentHub(IMediator mediator)
   {
      _mediator = mediator;
   }

   public async Task ConnectToGroup(Guid reviewId)
   {
      var connectionId = Context.ConnectionId;
      await Groups.AddToGroupAsync(connectionId, reviewId.ToString());
   }

   public async Task Notify(Guid reviewId, Guid commentId)
   {
      var connectionId = Context.ConnectionId;
      var getCommentQuery = new GetCommentQuery { CommentId = commentId };
      var comment = await _mediator.Send(getCommentQuery);
      
      await Clients.GroupExcept(reviewId.ToString(), connectionId)
         .SendAsync("CommentNotification", comment);
   }
}